using BookCatalog.DAL.Data;
using BookCatalog.DAL.Logging;
using BookCatalog.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServiceStack;


namespace BookCatalog.DAL.Repositories;


public class BookStoreRepository : GenericRepositoryAsync<BookStore>, IBookStoreRepository
{

    public BookStoreRepository(BookCatalogDbContext bookCatalogDbContext, ILogger<BookStoreRepository> logger) : base(bookCatalogDbContext, logger)
    {
    }

    public async Task<IEnumerable<BookStore>> Search(string query, BookStoreByOptions Orderby = BookStoreByOptions.Name, bool desc = false, bool? IsAvailable = null)
    {
        try
        {
            // Creating a variable to get the bookstore
            var result = Enumerable.Empty<BookStore>();
            IQueryable<BookStore> refactoredQuery;

            // Creating a variable to store the guid
            Guid guid;
            // Checking if the query is a guid and if it is save it in the guid variable
            if (Guid.TryParse(query, out guid))
            {
                // If the query is a guid, search for the bookstore by the id
                _logger.LogInformation(LoggingStrings.InfoSearchEntityID(_entityType.Name, guid));

                // creating the query for retrieving the bookstore by id
                refactoredQuery = _dbSet.Where(x => x.Id == guid);
                if (!refactoredQuery.Any())
                {
                    // logging that the entity was not found
                    _logger.LogWarning(LoggingStrings.WarningNoEntitiyIdFound(_entityType.Name, guid));
                }
            }

            // If the query is not a guid then search for the bookstore by the title, author, publisher or genre
            else
            {

                // Logging that the search is in progress with a query
                _logger.LogInformation(LoggingStrings.InfoSearchingEntitiesQuery(_entityType.Name, query));
                // Creating the query for retrieving the bookstore by the title, author, publisher or genre
                refactoredQuery = _dbSet.Where(x => x.Name.Contains(query)
                                      || x.PhoneNumber.Contains(query)
                                      || x.Email.Contains(query)
                                      || x.Address.Contains(query));
            }

            // checking that there are any entities
            if (refactoredQuery.Any())
            {
                // filtering and then ordering the entity
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

    // Order the books by a query
    public async Task<IEnumerable<BookStore>> OrderBookBy(BookStoreByOptions option = BookStoreByOptions.Name, bool desc = false)
    {
        try
        {
            // If no books are provided, use the database set
            IQueryable<BookStore> bookstoreQuery = _dbSet;
            return await OrderingQuery(bookstoreQuery, option, desc).ToListAsync();
        }
        catch (Exception ex)
        {
            // logging a general error
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("Ordering", _entityType.Name));
            throw;
        }

    }

    // Ordering the query
    private IQueryable<BookStore> OrderingQuery(IQueryable<BookStore> query, BookStoreByOptions option = BookStoreByOptions.Name, bool desc = false)
    {
        // Checking the option and ordering the books by the option
        switch (option)
        {

            // Ordering by the Address
            case BookStoreByOptions.Address:
                // Ordering the books in a descending way if the desc is true
                query = desc ? query.OrderByDescending(b => b.Address) : query.OrderBy(b => b.Address);
                break;

            // Ordering by the Email
            case BookStoreByOptions.Email:
                // Ordering the books in a descending way if the desc is true
                query = desc ? query.OrderByDescending(b => b.Email) : query.OrderBy(b => b.Email);
                break;

            // Ordering by the Name which is the default
            default:
                query = desc ? query.OrderByDescending(b => b.Name) : query.OrderBy(b => b.Name);
                break;
        }

        // Logging the sorting way, descending or ascending, and on which value
        _logger.LogInformation(LoggingStrings.InfoOrderingBy(_entityType.Name, option.ToString(), desc));
        return query;
    }
}
