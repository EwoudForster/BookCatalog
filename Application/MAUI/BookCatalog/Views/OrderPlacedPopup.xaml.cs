using CommunityToolkit.Maui.Views;

namespace BookCatalog.Views;

public partial class OrderPlacedPopup : Popup
{
    private OrderPlacedViewModel ViewModel;
    public OrderPlacedPopup(OrderPlacedViewModel orderPlacedViewModel)
	{
		InitializeComponent();
        ViewModel = orderPlacedViewModel;
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