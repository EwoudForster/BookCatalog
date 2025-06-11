using CommunityToolkit.Mvvm.ComponentModel;

namespace BookCatalog.Models
{
    public class CartItem : ObservableObject
    {
        private int _quantity;
        public int Quantity
        {
            get => _quantity;
            set
            {
                SetProperty(ref _quantity, value);
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        public Book Book { get; set; }

        public decimal TotalPrice => Book.Price * Quantity;
    }


}