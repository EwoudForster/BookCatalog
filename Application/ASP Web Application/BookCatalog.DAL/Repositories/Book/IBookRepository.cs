
using BookCatalog.DAL.Repositories.Generic.Async;

namespace BookCatalog.DAL.Repositories
{
    public interface IBookRepository : IRepositoryAsync<Book>
    {
        Task<IEnumerable<Book>> FilteringOn(Guid? Publisher = null, Guid? Author = null, Guid? Genre = null, bool? IsAvailable = null);
        Task<Dictionary<StatisticsOptions, object>> GetBookStatistics(BookByStatsOptions option = BookByStatsOptions.Price);
        Task<IEnumerable<IGrouping<object, Book>>> GroupBy(BookByOptions option = BookByOptions.Genre);
        Task<IEnumerable<Book>> OrderBookBy(BookByOptions option = BookByOptions.Title, bool desc = false);
        Task<IEnumerable<Book>> Search(string query, BookByOptions Orderby = BookByOptions.Title, bool desc = false, Guid? Publisher = null, Guid? Author = null, Guid? Genre = null, bool? IsAvailable = null);
    }
}