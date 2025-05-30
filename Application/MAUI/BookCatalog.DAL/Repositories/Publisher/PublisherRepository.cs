using BookCatalog.DAL.Data;
using BookCatalog.DAL.Logging;
using BookCatalog.DAL.Models;
using BookCatalog.DAL.Models.CalculatedValueModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookCatalog.DAL.Repositories
{
    public class PublisherRepository : GenericRepositoryAsync<Publisher>, IPublisherRepository
    {

        public PublisherRepository(BookCatalogDbContext context, ILogger<PublisherRepository> logger) : base(context, logger)
        {
        }

        // Get all entities with count of books per publisher
        public async Task<IEnumerable<PublisherCalculated>> GetAllWithBookStats()
        {
            try
            {
                // logging that we are getting the entities
                _logger.LogInformation(LoggingStrings.InfoRetrievingAllEntities(_entityType.Name));

                // variable to store the query
                var result = await _dbSet
                    .Select(publisher => new PublisherCalculated
                    {
                        Id = publisher.Id,
                        Name = publisher.Name,
                        Books = publisher.Books,
                        Average = publisher.Books.Any() ? publisher.Books.Average(b => b.Price) : 0m,
                        Max = publisher.Books.Any() ? publisher.Books.Max(b => b.Price) : 0m,
                        Min = publisher.Books.Any() ? publisher.Books.Min(b => b.Price) : 0m,
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


        public async Task<Dictionary<StatisticsOptions, object>> GetStatisticsForBooksPerPublisher(Guid PublisherId, BookByStatsOptions option = BookByStatsOptions.Price)
        {
            try
            {
                // Dictionary to store the statistics
                Dictionary<StatisticsOptions, object> statistics = new();
                var StatisticsType = "€";
                // variable to store the query
                Func<IQueryable<Publisher>, IQueryable<decimal>> query = x => x.Where(p => p.Id == PublisherId).SelectMany(b => b.Books).Select(b => b.Price);

                // Checking the option and getting the statistics by the option
                switch (option)
                {
                    case BookByStatsOptions.Year:
                        query = x => x.Where(p => p.Id == PublisherId).SelectMany(b => b.Books).Select(b => (decimal)b.PublicationYear);
                        StatisticsType = "Publication year";
                        break;

                    case BookByStatsOptions.Page:
                        query = x => x.Where(p => p.Id == PublisherId).SelectMany(b => b.Books).Select(b => (decimal)b.PageCount);
                        StatisticsType = "Page(s)";
                        break;

                    default:
                        break;
                }

                statistics.Add(StatisticsOptions.StatisticsType, StatisticsType);
                statistics.Add(StatisticsOptions.Average, await query(_dbSet).AverageAsync());
                statistics.Add(StatisticsOptions.TotalBooks, await _dbSet.Where(g => g.Id == PublisherId).Select(b => b.Books).CountAsync());
                statistics.Add(StatisticsOptions.Max, await query(_dbSet).MaxAsync());
                statistics.Add(StatisticsOptions.Min, await query(_dbSet).MinAsync());
                // return the values as a tuple
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
