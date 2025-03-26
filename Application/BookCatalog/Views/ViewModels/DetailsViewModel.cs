using BookCatalog.DataLayer;

namespace BookCatalog.Views.ViewModels
{
    public class DetailsViewModel
    {
        public Book Book { get; set; }

        public DetailsViewModel(Book book)
        {
            Book = book;
        }
    }
}
