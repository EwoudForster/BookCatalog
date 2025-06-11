using AutoMapper;
using BookCatalog.DAL.DTO;
using BookCatalog.DAL.Logging;
using BookCatalog.DAL.Models;
using BookCatalog.DAL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookCatalog.Web.Pages.Books
{
    [Authorize(Roles = "Administrator")]
    public class FormModel : PageModel
    {
        private readonly IBookRepository _bookRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IPublisherRepository _publisherRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IRepository<Picture> _pictureRepository;
        private readonly IMoreInfoRepository _moreInfoRepository;
        private readonly ILogger<FormModel> _logger;
        private readonly IMapper _mapper;


        public FormModel(IBookRepository bookRepository, IPublisherRepository publisherRepository, IGenreRepository genreRepository, IAuthorRepository authorRepository, IRepository<Picture> pictureRepository, IMoreInfoRepository moreInfoRepository, ILogger<FormModel> logger, IMapper mapper)
        {
            try
            {
                _logger = logger ?? throw new ArgumentNullException(nameof(logger), LoggingStrings.ErrorNullArgument("Logger"));
                _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository), LoggingStrings.ErrorNullArgument("BookRepository"));
                _genreRepository = genreRepository ?? throw new ArgumentNullException(nameof(genreRepository), LoggingStrings.ErrorNullArgument("GenreRepository"));
                _publisherRepository = publisherRepository ?? throw new ArgumentNullException(nameof(publisherRepository), LoggingStrings.ErrorNullArgument("publisherRepository"));
                _moreInfoRepository = moreInfoRepository ?? throw new ArgumentNullException(nameof(moreInfoRepository), LoggingStrings.ErrorNullArgument("moreInfoRepository"));
                _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository), LoggingStrings.ErrorNullArgument("authorRepository"));
                _pictureRepository = pictureRepository ?? throw new ArgumentNullException(nameof(pictureRepository), LoggingStrings.ErrorNullArgument("pictureRepository"));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), LoggingStrings.ErrorNullArgument("mapper"));
            }
            catch (Exception)
            {
                if (_logger != null)
                {
                    _logger.LogError(LoggingStrings.ErrorCreatingRepository("Book form"));
                }
                throw;
            }
        }

        [BindProperty]
        public bool IsEdit { get; set; }

        [BindProperty]
        public BookCreateDTO Book { get; set; }

        public SelectList AuthorSelectList { get; set; }
        public SelectList GenreSelectList { get; set; }
        public SelectList PublisherSelectList { get; set; }
        public SelectList MoreInfoSelectList { get; set; }
        public SelectList PictureSelectList { get; set; }



        public async Task OnGet(Guid BookId)
        {
            try
            {
                var EditBook = await _bookRepository.GetById(BookId);
                await LoadSelectListsAsync();
                if (EditBook == null)
                {
                    IsEdit = false;
                    ViewData["Title"] = "New Book";
                    Book = new BookCreateDTO();
                }
                else
                {
                    Book = _mapper.Map<BookCreateDTO>(EditBook);
                    Book.AuthorIds = EditBook.Authors.Select(a => a.Id).ToList();
                    Book.GenreIds = EditBook.Genres.Select(a => a.Id).ToList();
                    Book.PictureIds = EditBook.Pictures.Select(a => a.Id).ToList();
                    Book.MoreInfoIds = EditBook.MoreInfos.Select(a => a.Id).ToList();
                    IsEdit = true;
                    ViewData["Title"] = $"Editing {Book.Title}";

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("While Searching"));
                throw;
            }
        }


        public async Task<IActionResult> OnPost()
        {


            try
            {
                await LoadSelectListsAsync();

                if (!ModelState.IsValid)
                {
                    return Page();
                }
                (List<Author> SelectedAuthors, List<Genre> SelectedGenres, List<Picture> SelectedPictures, Publisher SelectedPublisher, List<MoreInfo> SelectedMoreInfos) = await GettingGenreAndAuthors(Book.AuthorIds, Book.GenreIds, Book.PictureIds, Book.PublisherId, Book.MoreInfoIds);

                // Use AutoMapper for flat fields
                var CreatedBook = _mapper.Map<Book>(Book);
                CreatedBook.Authors = SelectedAuthors;
                CreatedBook.Genres = SelectedGenres;
                CreatedBook.Pictures = SelectedPictures;
                CreatedBook.Publisher = SelectedPublisher;
                CreatedBook.MoreInfos = SelectedMoreInfos;

                if (IsEdit)
                {

                    await _bookRepository.Update(CreatedBook);
                }
                else
                {
                    await _bookRepository.Add(CreatedBook);
                }

                return Redirect($"/Books/Details/{CreatedBook.Id}");

            }
            catch (InvalidOperationException ex)
            {
                // Book already exists
                _logger.LogError(ex, LoggingStrings.ErrorAlreadyExists(nameof(Book), Book.Id));
                return BadRequest("Already exists");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("setting up form"));
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }

        }

        private async Task LoadSelectListsAsync()
        {
            var publishers = await _publisherRepository.GetAll();
            var authors = await _authorRepository.GetAll();
            var genres = await _genreRepository.GetAll();
            var images = await _pictureRepository.GetAll();
            var moreInfos = await _moreInfoRepository.GetAll();

            AuthorSelectList = new SelectList(authors, "Id", "Name");
            GenreSelectList = new SelectList(genres, "Id", "Name");
            PublisherSelectList = new SelectList(publishers, "Id", "Name");
            PictureSelectList = new SelectList(images, "Id", "ImgUrl");
            MoreInfoSelectList = new SelectList(moreInfos, "Id", "Name");
        }


        private async Task<(List<Author> SelectedAuthors, List<Genre> SelectedGenres, List<Picture> SelectedPictures, Publisher SelectedPublisher, List<MoreInfo> SelectedMoreInfos)> GettingGenreAndAuthors(List<Guid> AuthorIds, List<Guid> GenreIds, List<Guid> PictureIds, Guid PublisherId, List<Guid> MoreInfoIds)
        {
            var SelectedAuthors = new List<Author>();
            foreach (var author in AuthorIds)
            {
                SelectedAuthors.Add(await _authorRepository.GetById(author));
            }

            var SelectedGenres = new List<Genre>();
            foreach (var genreId in GenreIds)
            {
                SelectedGenres.Add(await _genreRepository.GetById(genreId));
            }

            var SelectedPictures = new List<Picture>();
            foreach (var pictureId in PictureIds)
            {
                SelectedPictures.Add(await _pictureRepository.GetById(pictureId));
            }
            var SelectedMoreInfos = new List<MoreInfo>();
            foreach (var MoreInfoId in MoreInfoIds)
            {
                SelectedMoreInfos.Add(await _moreInfoRepository.GetById(MoreInfoId));
            }

            var SelectedPublisher = new Publisher();
            SelectedPublisher = await _publisherRepository.GetById(PublisherId);


            return (SelectedAuthors, SelectedGenres, SelectedPictures, SelectedPublisher, SelectedMoreInfos);
        }
    }
}
