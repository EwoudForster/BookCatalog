namespace BookCatalog.Interfaces
{
    public interface ILoginService
    {
        Task<LoginResult> LoginAsync(string email, string password);
        Task<bool> RegisterAsync(string firstname, string lastname, string email, string password);
    }
}