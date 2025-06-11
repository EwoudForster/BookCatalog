using Newtonsoft.Json;

namespace BookCatalog.Services
{
    public class LoginService : ILoginService
    {

        private readonly IBookCatalogApiService _datastore;
        public async Task<LoginResult> LoginAsync(string email, string password)
        {
            var response = await _datastore.LoginAPI(email, password);
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<User>(jsonResponse);
                return new LoginResult { IsSuccess = true, UserId = user.Id };
            }

            return new LoginResult { IsSuccess = false };
        }

        public async Task<bool> RegisterAsync(string firstname, string lastname, string email, string password)
        {

            var response = await _datastore.RegisterAPI(firstname, lastname, email, password);
            return response.IsSuccessStatusCode;
        }

        public LoginService()
        {
            _datastore = ServiceHelper.GetService<IBookCatalogApiService>();
        }
    }
}
