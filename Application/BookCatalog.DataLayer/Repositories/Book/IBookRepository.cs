
namespace BookCatalog.DataLayer.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
        decimal GetAverage(BookByStatsOptions option = BookByStatsOptions.Price);
        int GetBookCountBy(BookByOptions option = BookByOptions.isAvailable);
        (decimal average, int totalBooks, decimal max, decimal min) GetBookStatistics(BookByStatsOptions option = BookByStatsOptions.Price);
        IEnumerable<IGrouping<object, Book>> GroupBy(BookByOptions option = BookByOptions.Genre);
        IEnumerable<Book> OrderBookBy(BookByOptions option = BookByOptions.Title);
        Book Search(Guid query, string? GenreToFilterOn = null);
        IEnumerable<Book> Search(string query, string? GenreToFilterOn = null);
    }
}