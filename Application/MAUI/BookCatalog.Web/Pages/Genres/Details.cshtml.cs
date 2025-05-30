using AutoMapper;
using BookCatalog.DAL;
using BookCatalog.DAL.DTO;
using BookCatalog.DAL.Logging;
using BookCatalog.DAL.Models;
using BookCatalog.DAL.Repositories;
using BookCatalog.Web.Pages.Books;
using BookCatalog.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;

namespace BookCatalog.Web.Pages.Genres
{
    [Authorize]
    public class DetailsModel : PageModel
    {

        private readonly IBookRepository _bookRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly ILogger<DetailsModel> _logger; 
        private readonly IMapper _mapper;


        public DetailsModel(IBookRepository bookRepository, IGenreRepository genreRepository, IMapper mapper, ILogger<DetailsModel> logger)
        {
            try
            {
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), LoggingStrings.ErrorNullArgument(nameof(mapper)));

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
        public GenreDTO Genre { get; set; }
        [BindProperty]
        public StatisticsViewModel Statistics { get; set; }
        public async Task<IActionResult> OnGet(Guid GenreId)
        {
            try
            {
                ViewData["Title"] = "Genre Details";
                Genre = _mapper.Map<GenreDTO>(await _genreRepository.GetById(GenreId));
                if (Genre == null)
                {
                    return NotFound();
                }
                Statistics = new StatisticsViewModel(await _genreRepository.GetStatisticsForBooksPerGenre(GenreId, BookByStatsOptions.Price),
                    "genre", "in this genre");
                return Page();


            }
            catch (InvalidOperationException ex)
            {
                // logging an error if the book is not found
                _logger.LogError(ex, LoggingStrings.ErrorDoesNotExists(nameof(Genre), GenreId));
                return NotFound("Doesn't exist");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("getting the page: Details"));
                throw;
            }

        }
    }
}
