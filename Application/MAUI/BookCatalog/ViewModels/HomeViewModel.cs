using BookCatalog.Models;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;
using BookCatalog.Resources;

namespace BookCatalog.ViewModels;

public class HomeViewModel : BaseViewModel

{

    private ObservableCollection<Book> _featuredBooks;
    public ObservableCollection<Book> FeaturedBooks
    {
        get => _featuredBooks;
        set => SetProperty(ref _featuredBooks, value);
    }

    private string _catalogSummary;
    public string CatalogSummary
    {
        get => _catalogSummary;
        set => SetProperty(ref _catalogSummary, value);
    }
    private Picture _heroImage;
    public Picture HeroImage
    {
        get => _heroImage;
        set => SetProperty(ref _heroImage, value);
    }
    private Picture _bookImage;
    public Picture BookImage
    {
        get => _bookImage;
        set => SetProperty(ref _bookImage, value);
    }

    private IBookCatalogApiService _bookCatalog;

    public IAsyncRelayCommand RefreshCommand { get; }

    public IAsyncRelayCommand LoadImagesCommand { get; }
    public IAsyncRelayCommand LoadCatalogSummaryCommand { get; }
    public HomeViewModel(IBookCatalogApiService bookCatalog)
    {
        LoadImagesCommand = new AsyncRelayCommand(LoadImages);
        LoadCatalogSummaryCommand = new AsyncRelayCommand(LoadCatalogSummary);
        _bookCatalog = bookCatalog;
        RefreshCommand = new AsyncRelayCommand(RefreshItemsAsync);

    }

    private async Task RefreshItemsAsync()
    {
        // Prevent multiple simultaneous refreshes
        if (IsLoading) return;

        IsRefreshing = true;
        IsLoading = true;

        try
        {
            await LoadImages();
            await LoadCatalogSummary();
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

    public async Task LoadCatalogSummary()
    {
        try
        {
            GeneralStatistics? generalStatistics = await _bookCatalog.GetGeneralStatistics();
            if (generalStatistics == null)
            {
                CatalogSummary = AppRes.CatalogSummaryError;
            }
            else
            {
                CatalogSummary = string.Format(
                    AppRes.CatalogSummaryTemplate,
                    generalStatistics.TotalBooks,
                    generalStatistics.TotalGenres,
                    generalStatistics.MinPrice.ToString("C"),
                    generalStatistics.MaxPrice.ToString("C"),
                    generalStatistics.AveragePrice.ToString("C"),
                    generalStatistics.TotalAuthors,
                    generalStatistics.TotalPublishers
                );
            }
        }
        catch (Exception ex)
        {

        }
    }

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
    public async Task LoadImages()
    {
        try
        {
            HeroImage = new(){
                Id = Guid.NewGuid(),
                ImgUrl = "homepage1.png"
            };
            BookImage = new(){
                Id = Guid.NewGuid(),
                ImgUrl = "book.png"
            };
        }
        catch (Exception ex)
        {

        }
    }
}
