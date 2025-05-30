
using BookCatalog.Services;

namespace BookCatalog.Interfaces
{
    public interface IBookCatalogApiService
    {
        Task DeleteItemAsync(string id, Endpoints endpoints = Endpoints.Books);
        Task<List<T>?> GetAllAsync<T>(Endpoints endpoints = Endpoints.Books) where T : class, new();
        Task<T?> GetByIdAsync<T>(Guid id, Endpoints endpoints = Endpoints.Books) where T : class, new();
        Task<GeneralStatistics?> GetGeneralStatistics();
        Task<HttpResponseMessage> LoginAPI(string email, string password);
        Task<HttpResponseMessage> RegisterAPI(string email, string password);
        Task SaveItemAsync<T>(T item, bool isNewItem = false, Endpoints endpoints = Endpoints.Books);
        Task<List<Book>?> SearchBooks(string query);
        Task<List<BookStore>?> SearchBookStores(string query);
    }
}