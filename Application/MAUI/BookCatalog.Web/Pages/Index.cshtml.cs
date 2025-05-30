using AutoMapper;
using BookCatalog.DAL;
using BookCatalog.DAL.DTO;
using BookCatalog.DAL.Logging;
using BookCatalog.DAL.Models;
using BookCatalog.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookCatalog.Web.Pages
{
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
                _logger = logger ?? throw new ArgumentNullException(nameof(logger), LoggingStrings.ErrorNullArgument("Logger"));
                _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository), LoggingStrings.ErrorNullArgument("BookRepository"));
                _genreRepository = genreRepository ?? throw new ArgumentNullException(nameof(genreRepository), LoggingStrings.ErrorNullArgument("GenreRepository"));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), LoggingStrings.ErrorNullArgument(nameof(mapper)));

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
        public IEnumerable<BookDTO> Carousel { get; set; }
        [BindProperty]
        public Dictionary<StatisticsOptions, object> StatisticsPrice { get; set; }

        public async Task<IActionResult> OnGet()
        {
            try
            {
                ViewData["Title"] = "Home";
                Books = _mapper.Map<IEnumerable<BookDTO>>(await _bookRepository.OrderBookBy(BookByOptions.Year));
                if (Books.Count() <= 0)
                {
                    return NotFound();
                }
                StatisticsPrice = await _bookRepository.GetBookStatistics(BookByStatsOptions.Price);  
                if(Books.Count() > 5)
                {  
                    Carousel = Books.Take(5);
                }
                else
                {
                    Carousel = Books;
                }
                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("getting the homepage"));
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
   
        }
    }
}
