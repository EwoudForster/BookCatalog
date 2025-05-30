using BookCatalog.DAL;
using BookCatalog.DAL.Repositories;
using BookCatalog.DAL.Repositories.Generic.Async;
using BookCatalog.DAL.Services.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.Web.Pages.Genres
{
    [Authorize(Roles = "Administrator")]
    public class FormModel : PageModel
    {
        private readonly IBookRepository _bookRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly ILogger<FormModel> _logger;


        public FormModel(IBookRepository bookRepository, IGenreRepository genreRepository, IAuthorRepository authorRepository, ILogger<FormModel> logger)
        {
            try
            {
                _logger = logger ?? throw new ArgumentNullException(nameof(logger), LoggingStrings.ErrorNullArgument("Logger"));
                _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository), LoggingStrings.ErrorNullArgument("BookRepository"));
                _genreRepository = genreRepository ?? throw new ArgumentNullException(nameof(genreRepository), LoggingStrings.ErrorNullArgument("GenreRepository"));
                _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository), LoggingStrings.ErrorNullArgument("AuthorRepository"));
            }
            catch (Exception ex)
            {
                if (_logger != null)
                {
                    _logger.LogError(ex, LoggingStrings.ErrorCreatingRepository("Book form"));
                }
                throw;
            }
        }

        [BindProperty]
        public bool IsEdit { get; set; }

        [BindProperty]
        public Genre Genre { get; set; }

        public async Task OnGet(Guid GenreId)
        {
            try
            {
                Genre = await _genreRepository.GetById(GenreId);
                if (Genre == null)
                {
                    Genre = new Genre();
                    IsEdit = false;
                    ViewData["Title"] = "New Genre";
                }
                else
                {
                   IsEdit = true;
                    ViewData["Title"] = $"Editing {Genre.Name}";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("setting up form"));
                throw;
            }
        }


        public async Task<IActionResult> OnPost()
        {
            try { 
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (ModelState.IsValid)
            {
                if (IsEdit)
                {
                    await _genreRepository.Update(Genre);
                }
                else
                {
                    await _genreRepository.Add(Genre);
                }
                return Redirect($"/Genres/Details/{Genre.Id}");
            }
            return Page();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("setting up form"));
                throw;
            }
        }
    }
}
