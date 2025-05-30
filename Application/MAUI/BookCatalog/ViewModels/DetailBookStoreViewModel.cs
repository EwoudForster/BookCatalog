using BookCatalog.Services;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
namespace BookCatalog.ViewModels;
public class DetailBookStoreViewModel : BaseViewModel
{
    private BookStore _selectedBookStore;
    private ObservableCollection<string> _pictures;
    private readonly IBookCatalogApiService _datastore;

    public IAsyncRelayCommand LoadBookStoreCommand { get; }

    public ObservableCollection<string> Pictures
    {
        get => _pictures;
        set => SetProperty(ref _pictures, value);
    }
    public BookStore SelectedBookStore
    {
        get => _selectedBookStore;
        set => SetProperty(ref _selectedBookStore, value);
    }

    private Guid _storeId;
    public Guid StoreId
    {
        get => _storeId;
        set => SetProperty(ref _storeId, value);
    }

    public IRelayCommand CloseCommand { get; }
    public IAsyncRelayCommand LoadPicturesCommand { get; }

    private bool _isLoading;
    public bool IsLoading
    {
        get => _isLoading;
        set => SetProperty(ref _isLoading, value);
    }

    public event Action? CloseRequested;

    public DetailBookStoreViewModel(Guid storeId)
    {
        _storeId = storeId;
        LoadBookStoreCommand = new AsyncRelayCommand(LoadBookStoreDetails);
        LoadPicturesCommand = new AsyncRelayCommand(LoadPictures);
        _datastore = ServiceHelper.GetService<IBookCatalogApiService>();

        CloseCommand = new RelayCommand(OnClose);
        Pictures = new ObservableCollection<string>();

    }

    private async Task LoadBookStoreDetails()
    {
        try
        {
            if (IsLoading) return;
            IsLoading = true;

            var store = await _datastore.GetByIdAsync<BookStore>(_storeId, Endpoints.BookStores);

            if (store != null)
            {
                SelectedBookStore = store;
            }
        }
        catch (Exception ex)
        {

        }
        finally
        {
            IsLoading = false;
        }
    }

    private async Task LoadPictures()
    {
        // Prevent multiple simultaneous refreshes
        if (IsLoading) return;
        IsLoading = true;

        try
        {
            Pictures.Clear();
            var selectedBooksStore = await _datastore.GetByIdAsync<BookStore>(_storeId, Endpoints.BookStores);

            var newList = new ObservableCollection<string>(selectedBooksStore.Pictures.Select(p => p.ImgUrl));
            Pictures = newList;

        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error refreshing books: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }
    

    private void OnClose()
    {
        CloseRequested?.Invoke();
    }
}
