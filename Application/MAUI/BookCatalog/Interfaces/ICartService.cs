
using System.Collections.ObjectModel;

namespace BookCatalog.Interfaces
{
    public interface ICartService
    {
        System.Collections.ObjectModel.ReadOnlyObservableCollection<CartItem> Items { get; }
            void AddToCart(Book book, int quantity);
            void RemoveFromCart(CartItem item);
            void ClearCart();
            void PlaceOrder();

    }
}