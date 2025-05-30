using BookCatalog.ViewModels;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;

namespace BookCatalog.Views;

public partial class BookStoresPage : ContentPage
{
    private BookStoresViewModel ViewModel;

    public BookStoresPage(BookStoresViewModel viewModel)
    {
        InitializeComponent();
        ViewModel = viewModel;

        BindingContext = viewModel;

        // Assign the Map control to the ViewModel's Map property
        ViewModel.Map = map;

        // Load bookstores once page is loaded
        Loaded += async (s, e) =>
        {
            if (ViewModel.GetBookStoresCommand.CanExecute(null))
                await ViewModel.GetBookStoresCommand.ExecuteAsync(null);
        };
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (ViewModel.GetBookStoresCommand.CanExecute(null))
            await ViewModel.GetBookStoresCommand.ExecuteAsync(null);
    }
}
