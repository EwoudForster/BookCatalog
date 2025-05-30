using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BookCatalog.ViewModels;

public class BooksViewModel : BaseViewModel
{
    public IAsyncRelayCommand RefreshCommand { get; }
    public IAsyncRelayCommand LoadBooksCommand { get; }
    private readonly IBookCatalogApiService _bookService;

    public ObservableCollection<Book> Books { get; private set; } = new();

    bool _isRefreshing;
    public bool IsRefreshing
    {
        get => _isRefreshing;
        set => SetProperty(ref _isRefreshing, value);
    }
    bool _isLoading;
    public bool IsLoading
    {
        get => _isLoading;
        set => SetProperty(ref _isLoading, value);
    }

    public BooksViewModel(IBookCatalogApiService bookService)
    {
        _bookService = bookService;
        RefreshCommand = new AsyncRelayCommand(RefreshItemsAsync);
        LoadBooksCommand = new AsyncRelayCommand(LoadBooks);
    }

    public async Task LoadBooks()
    {
        // Prevent multiple simultaneous loads
        if (IsLoading) return;

        IsLoading = true;
        try
        {
            var books = await _bookService.GetAllAsync<Book>();
            Books.Clear();
                foreach (var book in books)
                    Books.Add(book);
           
        }
        catch (Exception ex)
        {

            System.Diagnostics.Debug.WriteLine($"Error loading books: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    private async Task RefreshItemsAsync()
    {
        // Prevent multiple simultaneous refreshes
        if (IsLoading) return;

        IsRefreshing = true;
        IsLoading = true;

        try
        {
            Books.Clear();
            var books = await _bookService.GetAllAsync<Book>();

            foreach (var book in books)
                Books.Add(book);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error refreshing books: {ex.Message}");
        }
        finally
        {
            IsRefreshing = false;
            IsLoading = false;
        }
    }

    public Command<Book> BookTapped => new(async (book) =>
    {
        if (book == null)
            return;
        await Shell.Current.GoToAsync($"{nameof(BookPage)}?id={book.Id}");
    });
}