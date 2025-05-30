using BookCatalog.DAL.Models.CalculatedValueModel;
using BookCatalog.DAL.Repositories.Generic.Async;
using BookCatalog.DAL.Services.Logging;
using BookCatalog.DAL.Storage.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookCatalog.DAL.Repositories
{
    public class AuthorRepository : GenericRepositoryAsync<Author>, IAuthorRepository
    {

        public AuthorRepository(BookDbContext context, ILogger<AuthorRepository> logger) : base(context, logger)
        {
        }

        // Get all entities with count of books per author
        public async Task<IEnumerable<AuthorCalculated>> GetAllWithBookStats()
        {
            try
            {
                // logging that we are getting the entities
                _logger.LogInformation(LoggingStrings.InfoRetrievingAllEntities(_entityType.Name));

                // variable to store the query
                List<AuthorCalculated> result = await _dbSet
                    .Select(author => new AuthorCalculated
                    {
                        Id = author.Id,
                        Name = author.Name,
                        Books = author.Books,
                        Average = author.Books.Any() ? author.Books.Average(b => b.Price) : 0m,
                        Max = author.Books.Any() ? author.Books.Max(b => b.Price) : 0m,
                        Min = author.Books.Any() ? author.Books.Min(b => b.Price) : 0m,
                    })
                    .ToListAsync();


                if (result.Count() == 0)
                {
                    // logging that the db is empty
                    _logger.LogWarning(LoggingStrings.WarningNoEntitiesFound(_entityType.Name));
                }
                else
                {
                    // logging that we have gotten the entities
                    _logger.LogInformation(LoggingStrings.InfoRetrievedEntities(_entityType.Name, result.Count()));
                }

                return result;
            }
            // Catching any exceptions and logging them
            catch (Exception ex)
            {
                _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("retreiving", _entityType.Name));
                throw;
            }

        }

        public async Task<Dictionary<StatisticsOptions, object>> GetStatisticsForBooksPerAuthor(Guid AuthorId, BookByStatsOptions option = BookByStatsOptions.Price)
        {
            try
            {
                // Dictionary to store the statistics
                Dictionary<StatisticsOptions, object> statistics = new();
                var statisticsType = "€";

                // variable to store the query
                Func<IQueryable<Author>, IQueryable<decimal>> query = x => x.Where(g => g.Id == AuthorId).SelectMany(a => a.Books).Select(b => b.Price);

                // Checking the option and getting the statistics by the option
                switch (option)
                {
                    case BookByStatsOptions.Year:
                        query = x => x.Where(g => g.Id == AuthorId).SelectMany(a => a.Books).Select(b => (decimal)b.PublicationYear);
                        statisticsType = "Publication year";
                        break;

                    case BookByStatsOptions.Page:
                        query = x => x.Where(g => g.Id == AuthorId).SelectMany(a => a.Books).Select(b => (decimal)b.PageCount);
                        statisticsType = "Page(s)";
                        break;

                    default:
                        break;
                }

                statistics.Add(StatisticsOptions.StatisticsType, statisticsType);
                statistics.Add(StatisticsOptions.Average, await query(_dbSet).AverageAsync());
                statistics.Add(StatisticsOptions.TotalBooks, await _dbSet.Where(g => g.Id == AuthorId).SelectMany(a => a.Books).CountAsync());
                statistics.Add(StatisticsOptions.Max, await query(_dbSet).MaxAsync());
                statistics.Add(StatisticsOptions.Min, await query(_dbSet).MinAsync());

                // return the values as a dictionary
                return statistics;
            }
            catch (Exception ex)
            {
                // logging a general error
                _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("Statistics", _entityType.Name));
                throw;
            }
        }
    }
}
