using CommunityToolkit.Maui.Views;

namespace BookCatalog.Views;
public partial class DetailBookStorePopup : Popup
{
    private DetailBookStoreViewModel ViewModel;

    public DetailBookStorePopup(DetailBookStoreViewModel bookStoreViewModel)
    {
        InitializeComponent();
        ViewModel = bookStoreViewModel;

        BindingContext = ViewModel;
        ViewModel.CloseRequested += OnCloseRequested;
    }



    private void OnCloseRequested()
    {
        // Unsubscribe after close to avoid memory leaks
        ViewModel.CloseRequested -= OnCloseRequested;

        // Close the popup
        Close();
    }
}
