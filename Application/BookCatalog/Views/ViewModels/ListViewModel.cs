using BookCatalog.DataLayer;
using BookCatalog.DataLayer.Repositories;

namespace BookCatalog.Views.ViewModels
{
    public class ListViewModel
    {
        public IEnumerable<IGrouping<object, Book>> Books;

        public ListViewModel(IEnumerable<IGrouping<object, Book>> books)
        {
            Books = books;
        }
    }
}
