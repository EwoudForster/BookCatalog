namespace BookCatalog.Views;
public partial class LoginPage : ContentPage
{
    private readonly LoginViewModel _viewModel;

    public LoginPage(LoginViewModel loginViewModel)
    {
        InitializeComponent();
        _viewModel = loginViewModel;
        BindingContext = _viewModel;

    }
}
