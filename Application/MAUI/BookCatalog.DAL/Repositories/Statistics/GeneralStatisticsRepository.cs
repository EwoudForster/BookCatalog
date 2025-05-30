using BookCatalog.DAL.Data;
using BookCatalog.DAL.Logging;
using BookCatalog.DAL.Models;
using Castle.Core.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServiceStack;


namespace BookCatalog.DAL.Repositories;


public class GeneralStatisticsRepository : IGeneralStatisticsRepository
{

    // List of entities, creating a logger and declaring the file system
    private readonly ILogger<GenericRepositoryAsync<GeneralStatistics>> _logger;
    private readonly BookCatalogDbContext _dbContext;
    private GeneralStatistics GeneralStatistics = new();
    public GeneralStatisticsRepository(BookCatalogDbContext DbContext, ILogger<GenericRepositoryAsync<GeneralStatistics>> logger)
    {

        try
        {
            // declare the logger and the DbContext
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), LoggingStrings.ErrorNullArgument("Logger"));
            // DbContext declaration and checking if not null
            _dbContext = DbContext ?? throw new ArgumentNullException(nameof(DbContext), LoggingStrings.ErrorNullArgument("DbContext"));
            // if it is an ILogger then no logging
        }
        catch (Exception ex)
        {
            if (_logger != null)
            {
                _logger.LogError(ex, LoggingStrings.ErrorCreatingRepository(nameof(GeneralStatistics)));
            }
            throw;
        }
    }

    public async Task<GeneralStatistics> GetGeneralStatistics()
    {
        try
        {
            // logging that we are getting the entities
            _logger.LogInformation(LoggingStrings.InfoGettingTheStatistics());

            // price statistics
            await GetPriceStatistics();

            // total books
            await GetTotalBooks();

            // total authors
            await GetTotalAuthors();

            // total publishers
            await GetTotalPublishers();

            // total genres
            await GetTotalGenres();

            _logger.LogInformation(LoggingStrings.InfoRetrievedStatistitics());

            // getting the result
            return GeneralStatistics;
        }
        catch (Exception ex)
        {
            // logging a general error
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("retreiving all", nameof(GeneralStatistics)));
            throw;
        }
    }

    // Get the average price, total generalStatisticss, max price and min price
    private async Task GetPriceStatistics()
    {
        try
        {
            var result = await _dbContext.Set<Book>()
                .GroupBy(b => 1)
                .Select(g => new
                {
                    MaxPrice = g.Max(b => b.Price),
                    MinPrice = g.Min(b => b.Price),
                    AveragePrice = g.Average(b => b.Price)
                })
                .FirstAsync();

            GeneralStatistics.MaxPrice = (double)result.MaxPrice;
            GeneralStatistics.MinPrice = (double)result.MinPrice;
            GeneralStatistics.AveragePrice = (double)result.AveragePrice;

        }
        catch (Exception ex)
        {
            // logging a general error
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("retreiving price statistics", nameof(GeneralStatistics)));
            throw;
        }
    }
    private async Task GetTotalBooks()
    {
        try
        {
            IQueryable<Book> query = _dbContext.Set<Book>().AsQueryable();
            GeneralStatistics.TotalBooks = await query.CountAsync();
        }
        catch (Exception ex)
        {
            // logging a general error
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("retreiving total books", nameof(GeneralStatistics)));
            throw;
        }
    }
    private async Task GetTotalAuthors()
    {
        try
        {
            IQueryable<Author> query = _dbContext.Set<Author>().AsQueryable();
            GeneralStatistics.TotalAuthors = await query.CountAsync();
        }
        catch (Exception ex)
        {
            // logging a general error
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("retreiving total authors", nameof(GeneralStatistics)));
            throw;
        }
    }
    private async Task GetTotalPublishers()
    {
        try
        {
            IQueryable<Publisher> query = _dbContext.Set<Publisher>().AsQueryable();
            GeneralStatistics.TotalPublishers = await query.CountAsync();
        }
        catch (Exception ex)
        {
            // logging a general error
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("retreiving total publishers", nameof(GeneralStatistics)));
            throw;
        }
    }
    private async Task GetTotalGenres()
    {
        try
        {
            IQueryable<Genre> query = _dbContext.Set<Genre>().AsQueryable();
            GeneralStatistics.TotalGenres = await query.CountAsync();
        }
        catch (Exception ex)
        {
            // logging a general error
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("retreiving total genres", nameof(GeneralStatistics)));
            throw;
        }
    }
}
