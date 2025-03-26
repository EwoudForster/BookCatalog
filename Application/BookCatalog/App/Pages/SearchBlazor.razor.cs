using BookCatalog.DataLayer;
using BookCatalog.DataLayer.Repositories;
using Microsoft.AspNetCore.Components;
using ServiceStack.Text;

namespace BookCatalog.App.Pages
{
    public partial class SearchBlazor
    {
        private string _searchText = "";
        private string _selectedGenre = "";
        public IEnumerable<IGrouping<object, Book>> Genres;
        public string SelectedGenre
        {
            get => _selectedGenre;
            set
            {
                _selectedGenre = value;
                Search();
            }
        }
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                Search();
            }
        }

        public List<Book> FilteredBooks { get; set; } = new List<Book>();

        [Inject]
        public IBookRepository? BookRepository { get; set; }

        [Inject]
        private NavigationManager Navigation { get; set; }


        protected override void OnInitialized()
        {
            if (BookRepository is not null)
            {
                FilteredBooks = BookRepository.GetAll().ToList();
                Genres = BookRepository.GroupBy();
            }

            var query = Navigation.ToAbsoluteUri(Navigation.Uri).Query;
            var queryParams = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(query);

            if (queryParams.ContainsKey("searchText"))
            {
                _searchText = queryParams["searchText"];
            }

            Search(); // Call the search method to filter based on the search text

        }


        private void Search()
     {
            if (SelectedGenre != null && SelectedGenre != "Filter on Genre")
            {
                FilteredBooks = BookRepository is not null
     ? BookRepository.Search(SearchText, SelectedGenre).ToList()
     : new List<Book>();
            }
            else
            {
                FilteredBooks = BookRepository is not null
     ? BookRepository.Search(SearchText).ToList()
     : new List<Book>();
            }
        }
    }

}
