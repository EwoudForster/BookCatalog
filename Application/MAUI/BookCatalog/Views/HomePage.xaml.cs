namespace BookCatalog.Views;

public partial class HomePage : ContentPage
{    
    private bool _hasLoaded = false;
    private HomeViewModel ViewModel;
	public HomePage(HomeViewModel homeViewModel)
	{
		InitializeComponent();
        ViewModel = homeViewModel; 
		BindingContext = ViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (!_hasLoaded)
        {
            _hasLoaded = true;

            await ViewModel.LoadImagesCommand.ExecuteAsync(null);
            await ViewModel.LoadCatalogSummaryCommand.ExecuteAsync(null);
        }

}

}