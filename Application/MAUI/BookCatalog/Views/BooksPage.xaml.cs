namespace BookCatalog.Views;

public partial class BooksPage : ContentPage
{
    private BooksViewModel ViewModel;
    private bool _hasLoaded = false;

    public BooksPage(BooksViewModel booksViewModel)
    {
        InitializeComponent();
        ViewModel = booksViewModel;
        BindingContext = ViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Only load books once when the page first appears
        if (!_hasLoaded && ViewModel.LoadBooksCommand.CanExecute(null))
        {
            _hasLoaded = true;
            await ViewModel.LoadBooksCommand.ExecuteAsync(null);
        }
    }
}