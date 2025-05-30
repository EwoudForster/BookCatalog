using BookCatalog.DAL.Models;

namespace BookCatalog.DAL.Repositories
{
    public interface IBookStoreRepository : IRepository<BookStore>
    {
        Task<IEnumerable<BookStore>> OrderBookBy(BookStoreByOptions option = BookStoreByOptions.Name, bool desc = false);
        Task<IEnumerable<BookStore>> Search(string query, BookStoreByOptions Orderby = BookStoreByOptions.Name, bool desc = false, bool? IsAvailable = null);
    }
}