
namespace BookCatalog.Views;

public partial class CartPage : ContentPage
{
    private bool _hasLoaded = false;
    private CartPageViewModel ViewModel;
    public CartPage(CartPageViewModel cartPageViewModel)
	{
        InitializeComponent();
        ViewModel = cartPageViewModel;
        BindingContext = ViewModel;

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (!_hasLoaded)
        {
            _hasLoaded = true;

             ViewModel.LoadNumbersCommand.Execute(null);
        }

    }
}