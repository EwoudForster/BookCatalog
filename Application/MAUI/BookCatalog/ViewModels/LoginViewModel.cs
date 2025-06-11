using BookCatalog.Services;
using CommunityToolkit.Mvvm.Input;

namespace BookCatalog.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly ILoginService _loginService;

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

        public IAsyncRelayCommand LoginCommand { get; }

        public IRelayCommand NavigateToRegisterCommand { get; }

        public LoginViewModel(ILoginService loginService)
        {
            _loginService = loginService;
            LoginCommand = new AsyncRelayCommand(LoginAsync, () => !IsBusy);
            NavigateToRegisterCommand = new RelayCommand(NavigateToRegister);
        }

        private async Task LoginAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            LoginCommand.NotifyCanExecuteChanged();

            try
            {
                Message = string.Empty;

                if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
                {
                    Message = "Please enter both email and password.";
                    return;
                }

                var result = await _loginService.LoginAsync(Email.Trim(), Password);

                if (result.IsSuccess)
                {
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
                    Message = "Login failed. Check your credentials.";
                }
            }
            catch (Exception ex)
            {
                Message = $"Error: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
                LoginCommand.NotifyCanExecuteChanged();
            }
        }

        private void NavigateToRegister()
        {
            var tab = Shell.Current.Items.FirstOrDefault(i => i.Route == nameof(Views.RegisterPage));
            if (tab != null)
            {
                Shell.Current.CurrentItem = tab;
            }
        }
    }
}
