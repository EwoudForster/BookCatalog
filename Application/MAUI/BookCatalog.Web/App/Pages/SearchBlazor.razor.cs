using AutoMapper;
using BookCatalog.DAL;
using BookCatalog.DAL.DTO;
using BookCatalog.DAL.Logging;
using BookCatalog.DAL.Models;
using BookCatalog.DAL.Repositories;
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

        public IEnumerable<GenreDTO>? Genres { get; set; } = Enumerable.Empty<GenreDTO>();
        public IEnumerable<AuthorDTO>? Authors { get; set; } = Enumerable.Empty<AuthorDTO>();
        public IEnumerable<PublisherDTO>? Publishers { get; set; } = Enumerable.Empty<PublisherDTO>();
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
        public IEnumerable<BookDTO> FilteredBooks { get; set; } = new List<BookDTO>();

        [Inject]
        public IBookRepository BookRepository { get; set; }        
        [Inject]
        public IAuthorRepository AuthorRepository { get; set; }        
        [Inject]
        public IGenreRepository GenreRepository { get; set; }
        [Inject]
        public IPublisherRepository PublisherRepository { get; set; }
        [Inject]
        public IMapper Mapper { get; set; }

        [Inject]
        public ILogger<SearchBlazor> Logger { get; set; }

        [Inject]
        private NavigationManager Navigation { get; set; }


        protected async override Task OnInitializedAsync()
        {
            try
            {
                Genres = Mapper.Map<IEnumerable<GenreDTO>>(await GenreRepository.GetAll());
                Authors = Mapper.Map<IEnumerable<AuthorDTO>>(await AuthorRepository.GetAll());
                Publishers = Mapper.Map<IEnumerable<PublisherDTO>>(await PublisherRepository.GetAll());

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
                FilteredBooks = Mapper.Map<IEnumerable<BookDTO>>(await BookRepository.Search(SearchText, OrderBy, Desc, FilterPublisher, FilterAuthor, FilterGenre, FilterIsAvailable));
                StateHasChanged();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("searching books"));
            }
        }
    } 
}
