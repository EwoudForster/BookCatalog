using BookCatalog.DAL;
using BookCatalog.DAL.Repositories;
using BookCatalog.DAL.Repositories.Generic.Async;
using BookCatalog.DAL.Services.Logging;
using Microsoft.AspNetCore.Components;
using ServiceStack.Text;
using System.Threading.Tasks;

namespace BookCatalog.Web.App.Pages
{
    public partial class SearchBlazor
    {
        private string? _searchText = "";
        private Guid? _filterGenre = Guid.Empty;
        private Guid? _filterAuthor = Guid.Empty;
        private Guid? _filterPublisher = Guid.Empty;
        private bool _filterIsAvailable = false;
        private bool _desc = false;
        private BookByOptions _orderBy;

        public IEnumerable<Genre>? Genres { get; set; } = Enumerable.Empty<Genre>();
        public IEnumerable<Author>? Authors { get; set; } = Enumerable.Empty<Author>();
        public IEnumerable<Publisher>? Publishers { get; set; } = Enumerable.Empty<Publisher>();
        public Guid? FilterGenre
        {
            get => _filterGenre;
            set
            {
                _filterGenre = value;
                Search();
            }
        }

        public Guid? FilterPublisher
        {
            get => _filterPublisher;
            set
            {
                _filterPublisher = value;
                Search();
            }
        }

        public Guid? FilterAuthor
        {
            get => _filterAuthor;
            set
            {
                _filterAuthor = value;
                Search();
            }
        }

        public bool FilterIsAvailable
        {
            get => _filterIsAvailable;
            set
            {
                _filterIsAvailable = value;
                Search();
            }
        }

        public bool Desc
        {
            get => _desc;
            set
            {
                _desc = value;
                Search();
            }
        }

        public BookByOptions OrderBy
        {
            get => _orderBy;
            set
            {
                _orderBy = value;
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

        [Inject]
        public IEnumerable<Book> FilteredBooks { get; set; } = new List<Book>();

        [Inject]
        public IBookRepository BookRepository { get; set; }        
        [Inject]
        public IAuthorRepository AuthorRepository { get; set; }        
        [Inject]
        public IGenreRepository GenreRepository { get; set; }
        [Inject]
        public IPublisherRepository PublisherRepository { get; set; }

        [Inject]
        public ILogger<SearchBlazor> Logger { get; set; }

        [Inject]
        private NavigationManager Navigation { get; set; }


        protected async override Task OnInitializedAsync()
        {
            try
            {
                Genres = await GenreRepository.GetAll();
                Authors = await AuthorRepository.GetAll();
                Publishers = await PublisherRepository.GetAll();

                var query = Navigation.ToAbsoluteUri(Navigation.Uri).Query;
                var queryParams = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(query);

                if (queryParams.ContainsKey("searchText"))
                {
                    _searchText = queryParams["searchText"];
                }
      
                await Search();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("initializing search page"));
                throw;
            }
        }


        private async Task Search()
        {
            try
            {
                FilteredBooks = await BookRepository.Search(SearchText, OrderBy, Desc, FilterPublisher, FilterAuthor, FilterGenre, FilterIsAvailable);
                StateHasChanged();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("searching books"));
            }
        }
    } 
}
