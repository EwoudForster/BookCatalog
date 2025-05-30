namespace BookCatalog.Services;

public class BookStoreSearchHandler : SearchHandler
{
    // This gets set in the XAML on the page where the search handler is defined
    public Type? SelectedItemNavigationTarget { get; set; }

    protected override async void OnQueryChanged(string oldValue, string newValue)
    {
        base.OnQueryChanged(oldValue, newValue);

        if (string.IsNullOrWhiteSpace(newValue))
        {
            ItemsSource = null;
        }
        else
        {
            var dataStore = ServiceHelper.GetService<IBookCatalogApiService>();

            // Find the all of the leaders with a name that contains the search text
            ItemsSource = await dataStore.SearchBookStores(newValue);
        }
    }

    protected override async void OnItemSelected(object item)
    {
        base.OnItemSelected(item);

#if ANDROID
    // Workaround for https://github.com/dotnet/maui/issues/16298
    Unfocus();
    IsSearchEnabled = false;
    IsSearchEnabled = true;
#endif

        if (item is BookStore store)
        {
            var popupService = ServiceHelper.GetService<ISelfPopupService>();
            await popupService.ShowBottomPopupAsync(store);
        }
    }

    string? GetNavigationTarget()
    {
        // Find the first route for the specified page type
        return (Shell.Current as AppShell)?.Routes.FirstOrDefault(route => route.Value.Equals(SelectedItemNavigationTarget)).Key;
    }
}