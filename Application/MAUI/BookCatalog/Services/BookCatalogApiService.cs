using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace BookCatalog.Services;

public enum Endpoints
{
    Books,
    Authors,
    Genres,
    Publishers,
    Reviews,
    MoreInfo,
    Pictures,
    BookStores,
    Users,
    Logs
}

public class BookCatalogApiService : IBookCatalogApiService
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _serializerOptions;
    private readonly string prefixUrl = "https://2073-81-243-104-40.ngrok-free.app/api";


    public BookCatalogApiService()
    {
        _client = new HttpClient();
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
    }

    public async Task<T?> GetByIdAsync<T>(Guid id, Endpoints endpoints = Endpoints.Books) where T : class, new()
    {
        var uri = new Uri($"{prefixUrl}/{endpoints.ToString().ToLower()}/{id}");
        try
        {
            if (!await SetAuthorizationHeaderAsync())
            {
                Debug.WriteLine("No access token found.");
                return new T();
            }
            var response = await _client.GetAsync(uri);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                var refreshed = await RefreshAccessTokenAsync();
                if (refreshed)
                {
                    response = await _client.GetAsync(uri);
                }
            }
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return string.IsNullOrWhiteSpace(content)
                    ? new T()
                    : JsonSerializer.Deserialize<T>(content, _serializerOptions);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"GetByIdAsync Error: {ex.Message}");
        }

        return new T();
    }

    public async Task<List<T>?> GetAllAsync<T>(Endpoints endpoints = Endpoints.Books) where T : class, new()
    {
        var uri = new Uri($"{prefixUrl}/{endpoints.ToString().ToLower()}");
        try
        {
            if (!await SetAuthorizationHeaderAsync())
            {
                Debug.WriteLine("No access token found.");
                return new List<T>();
            }
            var response = await _client.GetAsync(uri);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                var refreshed = await RefreshAccessTokenAsync();
                if (refreshed)
                {
                    response = await _client.GetAsync(uri);
                }
            }
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return string.IsNullOrWhiteSpace(content)
                    ? new List<T>()
                    : JsonSerializer.Deserialize<List<T>>(content, _serializerOptions);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"GetAllAsync Error: {ex.Message}");
        }

        return new List<T>();
    }

    public async Task<GeneralStatistics?> GetGeneralStatistics()
    {
        var uri = new Uri($"{prefixUrl}/generalstatistics");
        try
        {
            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return string.IsNullOrWhiteSpace(content)
                    ? new GeneralStatistics()
                    : JsonSerializer.Deserialize<GeneralStatistics>(content, _serializerOptions);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"GetAllAsync Error: {ex.Message}");
        }

        return new GeneralStatistics();
    }

    public async Task SaveItemAsync<T>(T item, bool isNewItem = false, Endpoints endpoints = Endpoints.Books)
    {
        var uri = new Uri($"{prefixUrl}/{endpoints.ToString().ToLower()}");
        try
        {
            var json = JsonSerializer.Serialize(item, _serializerOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            if (!await SetAuthorizationHeaderAsync())
            {
                Debug.WriteLine("No access token found.");
                return;
            }

            HttpResponseMessage response = isNewItem
                ? await _client.PostAsync(uri, content)
                : await _client.PutAsync(uri, content);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                var refreshed = await RefreshAccessTokenAsync();
                if (refreshed)
                {
                    response = isNewItem
                        ? await _client.PostAsync(uri, content)
                        : await _client.PutAsync(uri, content);
                }
                else
                {
                    Debug.WriteLine("Failed to refresh token.");
                    return;
                }
            }

            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Item successfully saved.");
            }
            else
            {
                Debug.WriteLine($"Failed to save item: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"SaveItemAsync Error: {ex.Message}");
        }
    }


    public async Task DeleteItemAsync(string id, Endpoints endpoints = Endpoints.Books)
    {
        var uri = new Uri($"{prefixUrl}/{endpoints.ToString().ToLower()}/{id}");
        try
        {
            if (!await SetAuthorizationHeaderAsync())
            {
                Debug.WriteLine("No access token found.");
            }

            var response = await _client.DeleteAsync(uri);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                var refreshed = await RefreshAccessTokenAsync();
                if (refreshed)
                {
                    response = await _client.DeleteAsync(uri);
                }
            }
            if (!response.IsSuccessStatusCode)
                Debug.WriteLine($"Failed to delete item: {response.StatusCode}");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"DeleteItemAsync Error: {ex.Message}");
        }
    }

    public async Task<List<BookStore>?> SearchBookStores(string query)
    {
        var uri = new Uri($"{prefixUrl}/bookstoressearch");
        try
        {
            var json = JsonSerializer.Serialize(query, _serializerOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            if (!await SetAuthorizationHeaderAsync())
            {
                Debug.WriteLine("No access token found.");
                return new List<BookStore>();
            }

            HttpResponseMessage response = await _client.PostAsync(uri, content);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                var refreshed = await RefreshAccessTokenAsync();
                if (refreshed)
                {
                    response = await _client.PostAsync(uri, content);
                }
                else
                {
                    Debug.WriteLine("Failed to refresh token.");
                    return new List<BookStore>();
                }
            }

            if (response.IsSuccessStatusCode)
            {
                var responseStr = await response.Content.ReadAsStringAsync();
                return string.IsNullOrWhiteSpace(responseStr)
                    ? new List<BookStore>()
                    : JsonSerializer.Deserialize<List<BookStore>>(responseStr, _serializerOptions);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"SearchBookStores Error: {ex.Message}");
        }

        return new List<BookStore>();
    }

    public async Task<List<Book>?> SearchBooks(string query)
    {
        var uri = new Uri($"{prefixUrl}/bookssearch");
        try
        {
            var json = JsonSerializer.Serialize(query, _serializerOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            if (!await SetAuthorizationHeaderAsync())
            {
                Debug.WriteLine("No access token found.");
                return new List<Book>();
            }

            HttpResponseMessage response = await _client.PostAsync(uri, content);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                var refreshed = await RefreshAccessTokenAsync();
                if (refreshed)
                {
                    response = await _client.PostAsync(uri, content);
                }
                else
                {
                    Debug.WriteLine("Failed to refresh token.");
                    return new List<Book>();
                }
            }

            if (response.IsSuccessStatusCode)
            {
                var responseStr = await response.Content.ReadAsStringAsync();
                return string.IsNullOrWhiteSpace(responseStr)
                    ? new List<Book>()
                    : JsonSerializer.Deserialize<List<Book>>(responseStr, _serializerOptions);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"SearchBooks Error: {ex.Message}");
        }

        return new List<Book>();
    }


    public async Task<bool> PushOrderAsync(Order order)
    {
        var uri = new Uri($"{prefixUrl}/orders");
        try
        {
            var json = JsonSerializer.Serialize(order, _serializerOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            if (!await SetAuthorizationHeaderAsync())
            {
                Debug.WriteLine("No access token found.");
                return false;
            }

            var response = await _client.PostAsync(uri, content);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                var refreshed = await RefreshAccessTokenAsync();
                if (refreshed)
                {
                    response = await _client.PostAsync(uri, content);
                }
                else
                {
                    Debug.WriteLine("Failed to refresh token.");
                    return false;
                }
            }

            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Order successfully placed.");
                return true;
            }
            else
            {
                Debug.WriteLine($"Failed to place order: {response.StatusCode}");
                return false;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"PushOrderAsync Error: {ex.Message}");
            return false;
        }
    }


    private async Task<bool> SetAuthorizationHeaderAsync()
    {
        var accessToken = await SecureStorage.Default.GetAsync("access_token");
        if (!string.IsNullOrEmpty(accessToken))
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            return true;
        }
        return false;
    }

    private async Task<bool> RefreshAccessTokenAsync()
    {
        var refreshToken = await SecureStorage.Default.GetAsync("refresh_token");
        if (string.IsNullOrEmpty(refreshToken))
            return false;

        var refreshUri = new Uri($"{prefixUrl}/Auth/refresh");
        var request = new HttpRequestMessage(HttpMethod.Post, refreshUri)
        {
            Content = new FormUrlEncodedContent(new[]
            {
                    new KeyValuePair<string, string>("refresh_token", refreshToken)
                })
        };

        try
        {
            var response = await _client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(jsonResponse, _serializerOptions);
                if (tokenResponse != null && !string.IsNullOrEmpty(tokenResponse.AccessToken))
                {
                    await SecureStorage.Default.SetAsync("access_token", tokenResponse.AccessToken);
                    if (!string.IsNullOrEmpty(tokenResponse.RefreshToken))
                        await SecureStorage.Default.SetAsync("refresh_token", tokenResponse.RefreshToken);

                    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken);
                    return true;
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Refresh token failed: {ex.Message}");
        }
        return false;
    }

    public async Task<HttpResponseMessage> LoginAPI(string email, string password)
    {
        var uri = new Uri($"{prefixUrl}/Auth/login");
        try
        {
            var loginData = new { Email = email, Password = password };
            var json = JsonSerializer.Serialize(loginData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                var contentString = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(contentString, _serializerOptions);
                if (tokenResponse != null && !string.IsNullOrEmpty(tokenResponse.AccessToken))
                {
                    await SecureStorage.Default.SetAsync("access_token", tokenResponse.AccessToken);
                    if (!string.IsNullOrEmpty(tokenResponse.RefreshToken))
                        await SecureStorage.Default.SetAsync("refresh_token", tokenResponse.RefreshToken);
                }
            }
            return response;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Login Error: {ex.Message}");
            return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }
    }

    // Token response DTO - adjust this to your API response structure
    private class TokenResponse
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }

    public async Task<HttpResponseMessage> RegisterAPI(string lastname, string firstname, string email, string password)
    {
        var response = new HttpResponseMessage();

        var uri = new Uri($"{prefixUrl}/Auth/fullnameregister");
        try
        {
            var registerData = new { FirstName = firstname, LastName = lastname, Email = email, Password = password };
            var json = JsonSerializer.Serialize(registerData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            response = await _client.PostAsync(uri, content);
            return response;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Login Error: {ex.Message}");
        }
        return response;

    }
}
