using BookCatalog.Services;
using CommunityToolkit.Mvvm.Input;
using System.Text.RegularExpressions;

namespace BookCatalog.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private readonly ILoginService _loginService;

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }


        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);
        }

        private string _message;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public IAsyncRelayCommand RegisterCommand { get; }

        public RegisterViewModel(ILoginService loginService)
        {
            _loginService = loginService;
            RegisterCommand = new AsyncRelayCommand(RegisterAsync, () => !IsBusy);
        }

        private bool ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password)) return false;
            if (password.Length < 6) return false;
            if (!Regex.IsMatch(password, "[A-Z]")) return false;
            if (!Regex.IsMatch(password, "[0-9]")) return false;
            if (!Regex.IsMatch(password, @"[^a-zA-Z0-9]")) return false;
            return true;
        }

        private async Task RegisterAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            RegisterCommand.NotifyCanExecuteChanged();

            try
            {
                Message = string.Empty;

                if (string.IsNullOrWhiteSpace(Email) ||
                    string.IsNullOrWhiteSpace(Password) ||
                    string.IsNullOrWhiteSpace(ConfirmPassword))
                {
                    Message = "Please fill all fields.";
                    return;
                }

                if (Password != ConfirmPassword)
                {
                    Message = "Passwords do not match.";
                    return;
                }

                if (!ValidatePassword(Password))
                {
                    Message = "Password must be at least 6 characters long, contain at least 1 uppercase letter, 1 number, and 1 special character.";
                    return;
                }

                bool success = await _loginService.RegisterAsync(FirstName, LastName, Email, Password);
                if (success)
                {
                    LoginResult loginSuccess = await _loginService.LoginAsync(Email, Password);
                    if (loginSuccess.IsSuccess)
                    {
                        Message = "Registration successful. You are now logged in.";
                        UserSession.SetUser(Email);
                        MessagingCenter.Send<object>(this, "LoginStatusChanged");

                        var tab = Shell.Current.Items.FirstOrDefault(i => i.Route == nameof(Views.HomePage));
                        if (tab != null)
                        {
                            Shell.Current.CurrentItem = tab;
                        }
                    }
                    else
                    {
                        Message = "Registration succeeded but auto-login failed. Please login manually.";
                        var tab = Shell.Current.Items.FirstOrDefault(i => i.Route == nameof(Views.LoginPage));
                        if (tab != null)
                            Shell.Current.CurrentItem = tab;
                    }
                }
                else
                {
                    Message = "Registration failed. Try again.";
                }
            }
            finally
            {
                IsBusy = false;
                RegisterCommand.NotifyCanExecuteChanged();
            }
        }
    }
}
