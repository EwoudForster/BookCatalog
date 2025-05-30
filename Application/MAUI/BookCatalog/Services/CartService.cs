using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCatalog.Services;

    public class CartService : ICartService
    {
        private readonly ObservableCollection<CartItem> _items = new();

        public ReadOnlyObservableCollection<CartItem> Items => new(_items);

        public void AddToCart(Book book, int quantity)
        {
            var existingItem = _items.FirstOrDefault(ci => ci.Book.Id == book.Id);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                _items.Add(new CartItem { Book = book, Quantity = quantity });
            }
        }

        public void RemoveFromCart(CartItem item)
        {
            if (_items.Contains(item))
            {
                _items.Remove(item);
            }
        }

        public void ClearCart() => _items.Clear();

        public void PlaceOrder()
        {
        var datastore = ServiceHelper.GetService<IBookCatalogApiService>();
        
        ClearCart();
        }

}