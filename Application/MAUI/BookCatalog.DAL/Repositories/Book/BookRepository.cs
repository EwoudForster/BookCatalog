using BookCatalog.DAL.Data;
using BookCatalog.DAL.Logging;
using BookCatalog.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServiceStack;


namespace BookCatalog.DAL.Repositories;


public class BookRepository : GenericRepositoryAsync<Book>, IBookRepository
{

    public BookRepository(BookCatalogDbContext bookCatalogDbContext, ILogger<BookRepository> logger) : base(bookCatalogDbContext, logger)
    {
    }

    public async Task<IEnumerable<Book>> Search(string query, BookByOptions Orderby = BookByOptions.Title, bool desc = false, Guid? Publisher = null, Guid? Author = null, Guid? Genre = null, bool? IsAvailable = null)
    {
        try
        {
            // Creating a variable to get the books
            var result = Enumerable.Empty<Book>();
            IQueryable<Book> refactoredQuery;

            // Creating a variable to store the guid
            Guid guid;
            // Checking if the query is a guid and if it is save it in the guid variable
            if (Guid.TryParse(query, out guid))
            {
                // If the query is a guid, search for the book by the id
                _logger.LogInformation(LoggingStrings.InfoSearchEntityID(_entityType.Name, guid));

                // creating the query for retrieving the book by id
                refactoredQuery = _dbSet.Where(x => x.Id == guid);
                if (!refactoredQuery.Any())
                {
                    // logging that the entity was not found
                    _logger.LogWarning(LoggingStrings.WarningNoEntitiyIdFound(_entityType.Name, guid));
                }
            }

            // If the query is not a guid then search for the book by the title, author, publisher or genre
            else
            {

                // Logging that the search is in progress with a query
                _logger.LogInformation(LoggingStrings.InfoSearchingEntitiesQuery(_entityType.Name, query));
                // Creating the query for retrieving the book by the title, author, publisher or genre
                refactoredQuery = _dbSet.Where(x => x.Title.Contains(query)
                                      || x.Publisher.Name.Contains(query)
                                      || x.Authors.Any(a => a.Name.Contains(query))
                                      || x.Genres.Any(g => g.Name.Contains(query))
                                      || x.ISBN.Contains(query));
            }

            // checking that there are any entities
            if (refactoredQuery.Any())
            {
                // filtering and then ordering the entity
                refactoredQuery = FilteringQuery(refactoredQuery, Publisher, Author, Genre, IsAvailable);
                refactoredQuery = OrderingQuery(refactoredQuery, Orderby, desc);

                // getting the result
                result = await refactoredQuery.ToListAsync();

                // logging that we have gotten the entity
                _logger.LogInformation(LoggingStrings.InfoRetreivingEntitiesQuery(_entityType.Name, query));

            }
            else
            {
                // logging that the entity was not found
                _logger.LogWarning(LoggingStrings.WarningNoEntitiesFoundQuery(_entityType.Name, query));
            }
            return result;

        }
        catch (Exception ex)
        {
            // logging a general error
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("searching", _entityType.Name));
            throw;
        }
    }

    // Update entity
    public new async Task Update(Book item)
    {
        try
        {

            // setting the last updated date to the current time
            item.UpdatedAt = DateTime.Now;

            // logging that the item will be updated
            _logger.LogInformation(LoggingStrings.InfoUpdating(_entityType.Name, item.Id));

            // updating the item
            var existing = await _dbSet.FindAsync(item.Id);
            if (existing == null)
            {
                throw new DbUpdateConcurrencyException();
            }

            // Scalar updates
            _dbContext.Entry(existing).CurrentValues.SetValues(item);

            // Navigation updates (manual, even with lazy loading)
            existing.Authors.Clear();
            foreach (var author in item.Authors)
            {
                existing.Authors.Add(author);
            }

            existing.Genres.Clear();
            foreach (var genre in item.Genres)
            {
                existing.Genres.Add(genre);
            }

            existing.Pictures.Clear();
            foreach (var picture in item.Pictures)
            {
                existing.Pictures.Add(picture);
            }
            existing.MoreInfos.Clear();
            foreach (var moreinfo in item.MoreInfos)
            {
                existing.MoreInfos.Add(moreinfo);
            }

            existing.Publisher = item.Publisher; // if not null

            await Save();

            _dbContext.Entry(existing).CurrentValues.SetValues(item);
            // logging that the item will be updated
            _logger.LogInformation(LoggingStrings.InfoUpdateSucces(_entityType.Name, item.Id));

            // saving the changes asynchroniously
            await Save();

        }
        catch (DbUpdateConcurrencyException)
        {
            _logger.LogError(LoggingStrings.ErrorDoesNotExists(_entityType.Name, item.Id));
            throw;
        }
        catch (Exception ex)
        {
            // catching any exceptions and logging them
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("updating", _entityType.Name, item.Id));
            throw;
        }
    }

    // Filtering on option
    public async Task<IEnumerable<Book>> FilteringOn(Guid? Publisher = null, Guid? Author = null, Guid? Genre = null, bool? IsAvailable = null)
    {
        try
        {
            // If no books are provided, use the database set
            IQueryable<Book> booksQuery = _dbSet;
            var filteredQuery = FilteringQuery(booksQuery, Publisher, Author, Genre, IsAvailable);

            if (filteredQuery.Any())
            {
                // logging that we have gotten no entity
                _logger.LogWarning(LoggingStrings.WarningNoEntitiesFound(_entityType.Name));
            }
            return await filteredQuery.ToListAsync();
        }
        catch (Exception ex)
        {
            // logging a general error
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("Filtering", _entityType.Name));
            throw;
        }
    }


    // Order the books by a query
    public async Task<IEnumerable<Book>> OrderBookBy(BookByOptions option = BookByOptions.Title, bool desc = false)
    {
        try
        {
            // If no books are provided, use the database set
            IQueryable<Book> booksQuery = _dbSet;
            return await OrderingQuery(booksQuery, option, desc).ToListAsync();
        }
        catch (Exception ex)
        {
            // logging a general error
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("Ordering", _entityType.Name));
            throw;
        }

    }


    // Group the books by genre
    public async Task<IEnumerable<IGrouping<object, Book>>> GroupBy(BookByOptions option = BookByOptions.Genre)
    {
        try
        {
            // variable to store the query
            IQueryable<Book> query = _dbSet;
            return await GroupingQuery(query, option).ToListAsync();
        }
        catch (Exception ex)
        {
            // logging a general error
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("Grouping", _entityType.Name));
            throw;
        }
    }

    // Get the average price, total books, max price and min price
    public async Task<Dictionary<StatisticsOptions, object>> GetBookStatistics(BookByStatsOptions option = BookByStatsOptions.Price)
    {
        try
        {
            // Dictionary to store the statistics
            Dictionary<StatisticsOptions, object> statistics = new();
            var StatisticsType = "€";
            // variable to store the query
            Func<IQueryable<Book>, IQueryable<decimal>> query = x => x.Select(b => b.Price);

            // Checking the option and getting the statistics by the option
            switch (option)
            {
                case BookByStatsOptions.Year:
                    query = x => x.Select(b => (decimal)b.PublicationYear);
                    StatisticsType = "Publication year";
                    break;

                case BookByStatsOptions.Page:
                    query = x => x.Select(b => (decimal)b.PageCount);
                    StatisticsType = "Page(s)";
                    break;

                default:
                    break;
            }

            statistics.Add(StatisticsOptions.StatisticsType, StatisticsType);
            statistics.Add(StatisticsOptions.Average, await query(_dbSet).AverageAsync());
            statistics.Add(StatisticsOptions.TotalBooks, await _dbSet.CountAsync());
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

    // Grouping the query

    private IQueryable<IGrouping<object, Book>> GroupingQuery(IQueryable<Book> query, BookByOptions option = BookByOptions.Genre)
    {
        IQueryable<IGrouping<object, Book>> refactoredQuery;

        // Checking the option and grouping the books by the option
        switch (option)
        {
            case BookByOptions.Year:
                // Grouping the books by the year, the object cast is necessary to match the variable we set,
                // otherwise it will give a conflict
                refactoredQuery = query.GroupBy(b => (object)b.PublicationYear);
                break;

            case BookByOptions.Genre:
                // grouping the books by the genre
                refactoredQuery = query.GroupBy(b => (object)b.Genres);
                break;

            case BookByOptions.Price:
                // grouping the books by the price
                refactoredQuery = query.GroupBy(b => (object)b.Price);
                break;

            case BookByOptions.Publisher:
                // grouping the books by the publisher
                refactoredQuery = query.GroupBy(b => (object)b.Publisher);
                break;

            case BookByOptions.Page:
                // grouping the books by the page count
                refactoredQuery = query.GroupBy(b => (object)b.PageCount);
                break;

            case BookByOptions.Author:
                // grouping the books by the Author count
                refactoredQuery = query.GroupBy(b => (object)b.Authors);
                break;

            default:
                // grouping the books by the title which is the default
                refactoredQuery = query.GroupBy(b => (object)b.Title);
                break;
        }

        return refactoredQuery;
    }


    // Filtering the query
    private IQueryable<Book> FilteringQuery(IQueryable<Book> query, Guid? Publisher = null, Guid? Author = null, Guid? Genre = null, bool? IsAvailable = null)
    {
        string LoggingStringToFilterOn = "";
        // Filtering on Publisher if not null
        if (Publisher.HasValue && Publisher != Guid.Empty)
        {
            LoggingStringToFilterOn += $"Publisher: {Publisher}, ";
            query = query.Where(x => x.Publisher.Id == Publisher);
        }

        // Filtering on Author if not null
        if (Author.HasValue && Author != Guid.Empty)
        {
            LoggingStringToFilterOn += $"Author: {Author}, ";
            query = query.Where(x => x.Authors.Any(x => x.Id == Author));
        }

        // Filtering on Genre if not null
        if (Genre.HasValue && Genre != Guid.Empty)
        {
            LoggingStringToFilterOn += $"Genre: {Genre}, ";
            query = query.Where(x => x.Genres.Any(x => x.Id == Genre));
        }

        // Filtering on IsAvailable if not null
        if (IsAvailable == true)
        {
            LoggingStringToFilterOn += $"IsAvailable: {IsAvailable}, ";
            query = query.Where(x => x.IsAvailable.Equals(IsAvailable));
        }

        // Logging the filtering if there is any
        if (!string.IsNullOrWhiteSpace(LoggingStringToFilterOn))
        {
            _logger.LogInformation(LoggingStrings.InfoFilteringBy(_entityType.Name, LoggingStringToFilterOn));
        }
        return query;
    }

    // Ordering the query
    private IQueryable<Book> OrderingQuery(IQueryable<Book> query, BookByOptions option = BookByOptions.Title, bool desc = false)
    {
        // Checking the option and ordering the books by the option
        switch (option)
        {
            // Ordering by the year
            case BookByOptions.Year:
                // Ordering the books in a descending way if the desc is true
                query = desc ? query.OrderByDescending(b => b.PublicationYear) : query.OrderBy(b => b.PublicationYear);
                break;

            // Ordering by the price
            case BookByOptions.Price:
                // Ordering the books in a descending way if the desc is true
                query = desc ? query.OrderByDescending(b => b.Price) : query.OrderBy(b => b.Price);
                break;

            // Ordering by the publisher
            case BookByOptions.Publisher:
                // Ordering the books in a descending way if the desc is true
                query = desc ? query.OrderByDescending(b => b.Publisher) : query.OrderBy(b => b.Publisher);
                break;

            // Ordering by the page
            case BookByOptions.Page:

                // Ordering the books in a descending way if the desc is true
                query = desc ? query.OrderByDescending(b => b.PageCount) : query.OrderBy(b => b.PageCount);
                break;

            // Ordering by the page
            case BookByOptions.Author:

                // Ordering the books in a descending way if the desc is true
                query = desc ? query.OrderByDescending(b => b.Authors) : query.OrderBy(b => b.Authors);
                break;

            // Ordering by the title which is the default
            default:
                query = desc ? query.OrderByDescending(b => b.Title) : query.OrderBy(b => b.Title);
                break;
        }

        // Logging the sorting way, descending or ascending, and on which value
        _logger.LogInformation(LoggingStrings.InfoOrderingBy(_entityType.Name, option.ToString(), desc));
        return query;
    }
}
