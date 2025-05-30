using AutoMapper;
using BookCatalog.DAL;
using BookCatalog.DAL.DTO;
using BookCatalog.DAL.Logging;
using BookCatalog.DAL.Models;
using BookCatalog.DAL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookCatalog.Web.Pages.Books
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IBookRepository _bookRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly ILogger<IndexModel> _logger;
        private readonly IMapper _mapper;

        public IndexModel(IBookRepository bookRepository, IGenreRepository genreRepository, IMapper mapper, ILogger<IndexModel> logger)
        {
            try
            {
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), LoggingStrings.ErrorNullArgument("Mapper"));
                _logger = logger ?? throw new ArgumentNullException(nameof(logger), LoggingStrings.ErrorNullArgument("Logger"));
                _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository), LoggingStrings.ErrorNullArgument("BookRepository"));
                _genreRepository = genreRepository ?? throw new ArgumentNullException(nameof(genreRepository), LoggingStrings.ErrorNullArgument("GenreRepository"));
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
        public IEnumerable<BookDTO> Books { get; set; }
                [BindProperty]

        public IEnumerable<GenreDTO> Genres { get; set; }
        [BindProperty]
        public Guid? SelectedGenreId { get; set; }
        [BindProperty]
        public Guid sortSelect { get; set; }
        [BindProperty]
        public BookByOptions? SortOption { get; set; }

        public async Task<IActionResult> OnGet()
        {
            try
            {
                ViewData["Title"] = "Books";
                Books = _mapper.Map<IEnumerable<BookDTO>>(await _bookRepository.GetAll());
                Genres = _mapper.Map<IEnumerable<GenreDTO>>(await _genreRepository.GetAll());
                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("getting the homepage"));
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                Genres = _mapper.Map<IEnumerable<GenreDTO>>(await _genreRepository.GetAll());

                if (SelectedGenreId.HasValue && SelectedGenreId != Guid.Empty)
                {
                    Books = _mapper.Map<IEnumerable<BookDTO>>(
                        await _bookRepository.FilteringOn(Genre: SelectedGenreId));
                }
                else
                {
                    Books = _mapper.Map<IEnumerable<BookDTO>>(await _bookRepository.GetAll());
                }

                if (SortOption.HasValue)
                {
                    Books = _mapper.Map<IEnumerable<BookDTO>>(
                        await _bookRepository.OrderBookBy(SortOption.Value));
                }

                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("filtering books"));
                return StatusCode(500, "An unexpected error occurred while filtering books.");
            }
        }

    }
}
