namespace BookCatalog.Views;

public partial class RegisterPage : ContentPage
{
    private readonly RegisterViewModel _viewModel;

    public RegisterPage(RegisterViewModel registerViewModel)
    {
        InitializeComponent();
        _viewModel = registerViewModel;
        BindingContext = _viewModel;
    }
}
