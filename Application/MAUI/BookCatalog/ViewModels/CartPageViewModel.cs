using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace BookCatalog.ViewModels;

public partial class CartPageViewModel : BaseViewModel
{
    public ReadOnlyObservableCollection<CartItem> CartItems => _cartService.Items;
    private readonly ICartService _cartService;

    private decimal _grandTotal = 1;
    public decimal GrandTotal
    {
        get { return _grandTotal; }
        set { SetProperty(ref _grandTotal, value); }
    }
    private decimal _taxTotal = 1;
    public decimal TaxTotal
    {
        get { return _taxTotal; }
        set { SetProperty(ref _taxTotal, value); }
    }



    public IRelayCommand PlaceOrderCommand { get; }
    public IRelayCommand<CartItem> RemoveItemCommand { get; }
    public IRelayCommand ResetCartCommand { get; }
    public IRelayCommand LoadNumbersCommand { get; }

    public CartPageViewModel(ICartService cartService)
    {
        _cartService = cartService;

        PlaceOrderCommand = new RelayCommand(OnPlaceOrder);
        LoadNumbersCommand = new RelayCommand(UpdateTotals);
        RemoveItemCommand = new RelayCommand<CartItem>(OnRemoveItem);
        ResetCartCommand = new RelayCommand(OnResetCart);

    }

    private void OnRemoveItem(CartItem? item)
    {
        if (item != null)
            _cartService.RemoveFromCart(item);
        UpdateTotals();

    }

    private void OnResetCart()
    {
        _cartService.ClearCart();
        UpdateTotals();

    }
    private void UpdateTotals()
    {
        var subtotal = CartItems.Sum(item => item.TotalPrice);
        TaxTotal = subtotal * 0.21m;
        GrandTotal = subtotal + TaxTotal;
    }

    private async void OnPlaceOrder()
    {
        if (CartItems.Any())
        {
            await _cartService.PlaceOrder();
            UpdateTotals();


            var popup = new OrderPlacedPopup(new OrderPlacedViewModel());
            await Application.Current.MainPage.ShowPopupAsync(popup);
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Empty Cart", "Your cart is empty.", "OK");
        }
    }
}
