using BookCatalog.DataLayer.DataBase;
using BookCatalog.DataLayer.FileStorage.Filesystems;
using ServiceStack;
using System.Linq;

namespace BookCatalog.DataLayer.Repositories
{
    public enum BookByOptions
    {
        Genre,
        Year,
        Price,
        Publisher,
        Page,
        Title,
        isAvailable
    }


    public enum BookByStatsOptions
    {
        Year,
        Price,
        Page,
    }


    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(IFileSystem<Book> fileSystem) : base(fileSystem)
        {
        }

        public BookRepository(BookCatalogDbContext bookCatalogDbContext) : base(bookCatalogDbContext)
        {
        }

        // Search for a book by a query using the title, author, publisher or genre
        public IEnumerable<Book> Search(string query, string? GenreToFilterOn = null)
        {
            try
            {
 
                    Func<Book, bool> refactoredQuery =
                    x => x.Title.ToLower().Contains(query.ToLower())
                    || x.Publisher.ToLower().Contains(query.ToLower())
                    || x.Author.ToLower().Contains(query.ToLower())
                    || x.Genre.ToLower().Contains(query.ToLower())
                    || x.ISBN.ToLower().Contains(query.ToLower());
                if (!string.IsNullOrWhiteSpace(GenreToFilterOn))
                {
                    if (_bookCatalogDbContext != null)
                    {
                        return _bookCatalogDbContext.Set<Book>().Where(x => x.Genre == GenreToFilterOn).Where(refactoredQuery);
                    }

                    return _entities.Where(x => x.Genre == GenreToFilterOn).Where(refactoredQuery);
                }
                if (_bookCatalogDbContext != null)
                {
                    return _bookCatalogDbContext.Set<Book>().Where(refactoredQuery);
                }

                return _entities.Where(refactoredQuery);
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging library)
                _logger.Error(ex);
                return Enumerable.Empty<Book>(); // Return an empty list on error
            }
        }


        // Search for a book by a query using the id
        public Book Search(Guid query, string? GenreToFilterOn = null)
        {
            try
            {
                Func<Book, bool> refactoredQuery = x => x.Id == query;
                // Use LINQ to search for a book by its ID
                if (GenreToFilterOn != null) {
                    refactoredQuery = x => x.Id == query && x.Genre == GenreToFilterOn;

                }
                if (_bookCatalogDbContext != null)
                {
                    return _bookCatalogDbContext.Set<Book>().FirstOrDefault(refactoredQuery);
                }
                return _entities.FirstOrDefault(refactoredQuery);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return new Book();
            }
        }

        // Order the books by a query
        public IEnumerable<Book> OrderBookBy(BookByOptions option = BookByOptions.Title)
        {
            Func<Book, object> query = x => x.Title;
            switch (option)
            {
                case BookByOptions.Year:
                    query = x => x.PublicationYear;
                    break;

                case BookByOptions.Price:
                    query = x => x.Price;
                    break;

                case BookByOptions.Publisher:
                    query = x => x.Publisher;
                    break;

                case BookByOptions.Page:
                    query = x => x.PageCount;
                    break;

                default:
                    break;
            }

            if (_bookCatalogDbContext != null)
            {
                return _bookCatalogDbContext.Set<Book>().OrderBy(query);
            }
            return _entities.OrderBy(query);
        }

        // Group the books by genre
        public IEnumerable<IGrouping<object, Book>> GroupBy(BookByOptions option = BookByOptions.Genre)
        {
            Func<Book, object> query = x => x.Title;
            switch (option)
            {
                case BookByOptions.Year:
                    query = x => x.PublicationYear;
                    break; 
                
                case BookByOptions.Genre:
                    query = x => x.Genre;
                    break;

                case BookByOptions.Price:
                    query = x => x.Price;
                    break;

                case BookByOptions.Publisher:
                    query = x => x.Publisher;
                    break;

                case BookByOptions.Page:
                    query = x => x.PageCount;
                    break;

                default:
                    break;
            }

            if (_bookCatalogDbContext != null)
            {
                return _bookCatalogDbContext.Set<Book>().GroupBy(query);
            }
            return _entities.GroupBy(query);
        }

        // Get the average price, total books, max price and min price
        public (decimal average, int totalBooks, decimal max, decimal min) GetBookStatistics(BookByStatsOptions option = BookByStatsOptions.Price)
        {
            Func<Book, decimal> query = x => x.Price;
            decimal averagePrice;
            int totalBooks;
            decimal maxPrice;
            decimal minPrice;

            switch (option)
            {
                case BookByStatsOptions.Year:
                    query = x => x.PublicationYear;
                    break;

                case BookByStatsOptions.Page:
                    query = x => x.PageCount;
                    break;

                default:
                    break;
            }


            if (_bookCatalogDbContext != null)
            {
                averagePrice = _bookCatalogDbContext.Set<Book>().Average(query);
                totalBooks = _bookCatalogDbContext.Set<Book>().Count();
                maxPrice = _bookCatalogDbContext.Set<Book>().Max(query);
                minPrice = _bookCatalogDbContext.Set<Book>().Min(query);
            }

            else
            {

                averagePrice = _entities.Average(query);
                totalBooks = _entities.Count();
                maxPrice = _entities.Max(query);
                minPrice = _entities.Min(query);
            }
                // return the values as a tuple
                return (averagePrice, totalBooks, maxPrice, minPrice);
        }

        // Get the average page count
        public decimal GetAverage(BookByStatsOptions option = BookByStatsOptions.Price)
        {
            Func<Book, decimal> query = x => x.Price;
            switch (option)
            {
                case BookByStatsOptions.Year:
                    query = x => x.PublicationYear;
                    break;

                case BookByStatsOptions.Page:
                    query = x => x.PageCount;
                    break;

                default:
                    break;
            }
            if (_bookCatalogDbContext != null)
            {
                return (decimal)_bookCatalogDbContext.Set<Book>().Average(query);
            }
            return (decimal)_entities.Average(x => x.PageCount);
        }

        // Get the average publication year
        public int GetBookCountBy(BookByOptions option = BookByOptions.isAvailable)
        {

            Func<Book, bool> query = x => x.IsAvailable;
            switch (option)
            {
                case BookByOptions.Year:
                    query = x => x.PublicationYear > 0;
                    break;

                case BookByOptions.Price:
                    query = x => x.Price > 0;
                    break;

                case BookByOptions.Publisher:
                    query = x => !string.IsNullOrEmpty(x.Publisher);
                    break;

                case BookByOptions.Page:
                    query = x => x.PageCount > 0;
                    break;

                default:
                    break;
            }
            if (_bookCatalogDbContext != null)
            {
                return _bookCatalogDbContext.Set<Book>().Count(query);
            }
            return _entities.Count(query);
        }
    }
}
