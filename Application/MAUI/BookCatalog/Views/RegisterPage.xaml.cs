using BookCatalog.Services;

namespace BookCatalog.Views;

public partial class RegisterPage : ContentPage
{

        private readonly ILoginService _loginService;

        public RegisterPage()
        {
            InitializeComponent();
        _loginService = ServiceHelper.GetService<ILoginService>();

    }

    private async void OnRegisterClicked(object sender, EventArgs e)
        {
            string email = EmailEntry.Text?.Trim();
            string password = PasswordEntry.Text;
            string confirmPassword = ConfirmPasswordEntry.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageLabel.Text = "Please fill all fields.";
                return;
            }

            if (password != confirmPassword)
            {
                MessageLabel.Text = "Passwords do not match.";
                return;
            }

            bool success = await _loginService.RegisterAsync(email, password);
            if (success)
            {
                await DisplayAlert("Success", "Registration successful. Please login.", "OK");
            var tab = Shell.Current.Items.FirstOrDefault(i => i.Route == nameof(LoginPage));
            if (tab != null)
            {
                Shell.Current.CurrentItem = tab;
            }
        }
            else
            {
                MessageLabel.Text = "Registration failed. Try again.";
            }
        }
}