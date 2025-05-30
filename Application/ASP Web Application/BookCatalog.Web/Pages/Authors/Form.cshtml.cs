using BookCatalog.DAL;
using BookCatalog.DAL.Repositories;
using BookCatalog.DAL.Repositories.Generic.Async;
using BookCatalog.DAL.Services.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.Web.Pages.Authors
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
        public Author Author { get; set; }

        public async Task OnGet(Guid AuthorId)
        {
            try
            {
                Author = await _authorRepository.GetById(AuthorId);
                if (Author == null)
                {
                    Author = new Author();
                    IsEdit = false;                
                    ViewData["Title"] = "New Authors";

                }
                else
                {
                    ViewData["Title"] = $"Editing {Author.Name}";
                    IsEdit = true;
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
                    await _authorRepository.Update(Author);
                }
                else
                {
                    await _authorRepository.Add(Author);
                }
                return Redirect($"/Authors/Details/{Author.Id}");
            }
            return Page();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, LoggingStrings.ErrorDoesNotExists(nameof(Author), Author.Id));
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("setting up form"));
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }
    }
}
