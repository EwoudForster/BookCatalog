using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Cors.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookCatalog.ViewModels
{
    public class BookViewModel : BaseViewModel
    {
        private readonly IBookCatalogApiService _bookService;
        private Book _book;


        public Book Book
        {
            get { return _book; }
            set { SetProperty(ref _book, value); }
        }
        private readonly ICartService _cartService;

        private int _quantity = 1;
        public int Quantity
        {
            get { return _quantity; }
            set { SetProperty(ref _quantity, value); }
        }
        private decimal _subTotal = 1;
        public decimal SubTotal
        {
            get { return _subTotal; }
            set { SetProperty(ref _subTotal, value); }
        }
        private decimal _tax = 1;
        public decimal Tax
        {
            get { return _tax; }
            set { SetProperty(ref _tax, value); }
        }

        public ICommand AddToCartCommand { get; }

        public async Task LoadBookAsync(Guid id)
        {
            var book = await _bookService.GetByIdAsync<Book>(id);
            if (book != null)
            {
                Book = book;
            }
            LoadNumbers();

        }

        public IAsyncRelayCommand GoBackCommand => new AsyncRelayCommand(GoBackAsync);

        public IRelayCommand IncreaseQuantityCommand { get; }
        public IRelayCommand DecreaseQuantityCommand { get; }
        public BookViewModel(IBookCatalogApiService bookCatalogApiService, ICartService cartService)
        {
            _cartService = cartService;
            _bookService = bookCatalogApiService;
            IncreaseQuantityCommand = new RelayCommand(() => NumberUp());
            DecreaseQuantityCommand = new RelayCommand(() => NumberDown() );
            AddToCartCommand = new RelayCommand(OnAddToCart);
        }

        private void NumberUp()
        {
            Quantity++;
            LoadNumbers();
        }
        private void NumberDown()
        {
            if (Quantity > 1) {
                Quantity--;
        }
        LoadNumbers();
        }

        private void LoadNumbers() {
               SubTotal = Book?.Price * Quantity ?? 0;
                Tax = Book?.Price * 0.21m ?? 0;
        }

        private void OnAddToCart()
        {
            if (Book != null && Quantity > 0)
            {
                _cartService.AddToCart(Book, Quantity);

                Application.Current.MainPage.DisplayAlert("Success", "Book added to cart!", "OK");
            }
        }

        private async Task GoBackAsync()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
