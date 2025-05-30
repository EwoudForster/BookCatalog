namespace BookCatalog.Views;

[QueryProperty(nameof(ID), "id")]
public partial class BookPage : ContentPage
{
    private readonly BookViewModel _bookViewModel;

    public string ID
    {
        set
        {
            if (Guid.TryParse(value, out Guid parsedId))
            {
                _ = LoadBookAsync(parsedId);
            }
            else
            {
                Console.WriteLine("Invalid GUID passed to BookPage.");
            }
        }
    }

    public BookPage(BookViewModel bookViewModel)
    {
        InitializeComponent();
        _bookViewModel = bookViewModel;
        BindingContext = _bookViewModel;
    }

    private async Task LoadBookAsync(Guid id)
    {
        try
        {
            await _bookViewModel.LoadBookAsync(id);
        }
        catch (Exception ex)
        {
            // Handle or log the error
            Console.WriteLine($"Failed to load book: {ex.Message}");
        }
    }
}
