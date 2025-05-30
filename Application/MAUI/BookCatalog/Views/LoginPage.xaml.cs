using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using BookCatalog.Models;
using BookCatalog.Services;

namespace BookCatalog.Views;
public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();

        _loginService = ServiceHelper.GetService<ILoginService>();


    }
    private readonly ILoginService _loginService;

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string username = UsernameEntry.Text?.Trim();
        string password = PasswordEntry.Text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            MessageLabel.Text = "Please enter both username and password.";
            return;
        }

        try
        {
            var result = await _loginService.LoginAsync(username, password);

            if (result.IsSuccess)
            {
                UserSession.SetUser(result.UserId);
                MessagingCenter.Send<object>(this, "LoginStatusChanged");
                var tab = Shell.Current.Items.FirstOrDefault(i => i.Route == nameof(HomePage));
                if (tab != null)
                {
                    Shell.Current.CurrentItem = tab;
                }
            }
            else
            {
                MessageLabel.Text = "Login failed. Check your credentials.";
            }
        }
        catch (Exception ex)
        {
            MessageLabel.Text = $"Error: {ex.Message}";
        }
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        var tab = Shell.Current.Items.FirstOrDefault(i => i.Route == nameof(RegisterPage));
        if (tab != null)
        {
            Shell.Current.CurrentItem = tab;
        }
    }


}
