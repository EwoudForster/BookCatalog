namespace BookCatalog.Services;

public class BookSearchHandler : SearchHandler
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
            ItemsSource = await dataStore.SearchBooks(newValue);
        }
    }

    protected override async void OnItemSelected(object item)
    {
        base.OnItemSelected(item);

#if ANDROID
        // Work around for https://github.com/dotnet/maui/issues/16298
        Unfocus();
        IsSearchEnabled = false;
        IsSearchEnabled = true;
#endif
        var navigationTarget = GetNavigationTarget();

        // Use pattern matching to use this custom search handler for multiple data types
        if (item is Book book)
        {
            await Shell.Current.GoToAsync($"{navigationTarget}?id={book.Id}");
        }
    }
    string? GetNavigationTarget()
    {
        // Find the first route for the specified page type
        return (Shell.Current as AppShell)?.Routes.FirstOrDefault(route => route.Value.Equals(SelectedItemNavigationTarget)).Key;
    }
}