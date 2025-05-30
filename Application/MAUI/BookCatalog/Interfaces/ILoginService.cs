namespace BookCatalog.Interfaces
{
    public interface ILoginService
    {
        Task<LoginResult> LoginAsync(string username, string password);
        Task<bool> RegisterAsync(string email, string password);

    }
}