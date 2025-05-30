using BookCatalog.DAL.Models.CalculatedValueModel;
using BookCatalog.DAL.Repositories.Generic.Async;
using BookCatalog.DAL.Services.Logging;
using BookCatalog.DAL.Storage.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCatalog.DAL.Repositories
{
    public class GenreRepository : GenericRepositoryAsync<Genre>, IGenreRepository
    {

        public GenreRepository(BookDbContext context, ILogger<GenreRepository> logger) : base(context, logger)
        {
        }

        // Get all entities with count of books per genre
        public async Task<IEnumerable<GenreCalculated>> GetAllWithBookStats()
        {
            try
            {
                // logging that we are getting the entities
                _logger.LogInformation(LoggingStrings.InfoRetrievingAllEntities(_entityType.Name));

                // variable to store the query
                var result = await _dbSet
                    .Select(genre => new GenreCalculated
                    {
                        Id = genre.Id,
                        Name = genre.Name,
                        Books = genre.Books,
                        Average = genre.Books.Any() ? genre.Books.Average(b => b.Price) : 0m,
                        Max = genre.Books.Any() ?  genre.Books.Max(b => b.Price): 0m,
                        Min = genre.Books.Any() ? genre.Books.Min(b => b.Price) :0m,
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

        public async Task<Dictionary<StatisticsOptions, object>> GetStatisticsForBooksPerGenre(Guid GenreId, BookByStatsOptions option = BookByStatsOptions.Price)
        {
            try
            {
                // Dictionary to store the statistics
                Dictionary<StatisticsOptions, object> statistics = new();
                var StatisticsType = "€";
                // variable to store the query
                Func<IQueryable<Genre>, IQueryable<decimal>> query = x => x.Where(g => g.Id == GenreId).SelectMany(b => b.Books).Select(b => b.Price);

                // Checking the option and getting the statistics by the option
                switch (option)
                {
                    case BookByStatsOptions.Year:
                        query = x => x.Where(g => g.Id == GenreId).SelectMany(b => b.Books).Select(b => (decimal)b.PublicationYear);
                        StatisticsType = "Publication year";
                        break;

                    case BookByStatsOptions.Page:
                        query = x => x.Where(g => g.Id == GenreId).SelectMany(b => b.Books).Select(b => (decimal)b.PageCount);
                        StatisticsType = "Page(s)";
                        break;

                    default:
                        break;
                }

                statistics.Add(StatisticsOptions.StatisticsType, StatisticsType);
                statistics.Add(StatisticsOptions.Average, await query(_dbSet).AverageAsync());
                statistics.Add(StatisticsOptions.TotalBooks, await _dbSet.Where(g => g.Id == GenreId).Select(b => b.Books).CountAsync());
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
