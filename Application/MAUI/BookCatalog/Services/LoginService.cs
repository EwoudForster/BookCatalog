using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<bool> RegisterAsync(string email, string password)
        {

            var response = await _datastore.RegisterAPI(email, password);
            return response.IsSuccessStatusCode;
        }

        public LoginService()
        {
            _datastore = ServiceHelper.GetService<IBookCatalogApiService>();
        }
    }
}
