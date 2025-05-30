using AutoMapper;
using BookCatalog.DAL;
using BookCatalog.DAL.DTO;
using BookCatalog.DAL.Repositories;
using BookCatalog.DAL.Repositories.Generic.Async;
using BookCatalog.DAL.Services.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;

namespace BookCatalog.Web.Pages.Books
{
    [Authorize(Roles = "Administrator")]
    public class FormModel : PageModel
    {
        private readonly IBookRepository _bookRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IPublisherRepository _publisherRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly ILogger<FormModel> _logger;
        private readonly IMapper _mapper;


        public FormModel(IBookRepository bookRepository, IPublisherRepository publisherRepository, IGenreRepository genreRepository, IAuthorRepository authorRepository, ILogger<FormModel> logger, IMapper mapper)
        {
            try
            {
                _logger = logger ?? throw new ArgumentNullException(nameof(logger), LoggingStrings.ErrorNullArgument("Logger"));
                _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository), LoggingStrings.ErrorNullArgument("BookRepository"));
                _genreRepository = genreRepository ?? throw new ArgumentNullException(nameof(genreRepository), LoggingStrings.ErrorNullArgument("GenreRepository"));
                _publisherRepository = publisherRepository ?? throw new ArgumentNullException(nameof(publisherRepository), LoggingStrings.ErrorNullArgument("publisherRepository"));
                _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository), LoggingStrings.ErrorNullArgument("authorRepository"));
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


    
        public async Task OnGet(Guid BookId)
        {
            try
            {
                var EditBook = await _bookRepository.GetById(BookId);
                var publishers = await _publisherRepository.GetAll();
                var authors = await _authorRepository.GetAll();
                var genres = await _genreRepository.GetAll();

                AuthorSelectList = new SelectList(authors, "Id", "Name");
                GenreSelectList = new SelectList(genres, "Id", "Name");
                PublisherSelectList = new SelectList(publishers, "Id", "Name");
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
                (List<Author> SelectedAuthors, List<Genre> SelectedGenres) = await GettingGenreAndAuthors(Book.AuthorIds, Book.GenreIds);

                if (!ModelState.IsValid)
                {
                    var publishers = await _publisherRepository.GetAll();
                    var authors = await _authorRepository.GetAll();
                    var genres = await _genreRepository.GetAll();

                    AuthorSelectList = new SelectList(authors, "Id", "Name");
                    GenreSelectList = new SelectList(genres, "Id", "Name");
                    PublisherSelectList = new SelectList(publishers, "Id", "Name");
                    return Page();
                }
                // Create the new book
                var NewBook = new Book
                {
                    Title = Book.Title,

                    Authors = SelectedAuthors,
                    Genres = SelectedGenres,
                    PublisherId = Book.PublisherId,
                    PublicationYear = Book.PublicationYear,
                    Price = Book.Price,
                    PageCount = Book.PageCount,
                    ISBN = Book.ISBN,
                    ImgUrl = Book.ImgUrl,
                    IsAvailable = Book.IsAvailable
                };
                if (IsEdit)
                {
                    // Update the existing book
                    NewBook.Id = Book.Id;
                    await _bookRepository.Update(NewBook);
                }
                else
                {
                    await _bookRepository.Add(NewBook);
                }

                return Redirect($"/Books/Details/{NewBook.Id}");

            }
            catch (InvalidOperationException ex){
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

        private async Task<(List<Author> SelectedAuthors, List<Genre> SelectedGenres)> GettingGenreAndAuthors(List<Guid> AuthorIds, List<Guid> GenreIds)
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

            return (SelectedAuthors, SelectedGenres);
        }
    }
}
