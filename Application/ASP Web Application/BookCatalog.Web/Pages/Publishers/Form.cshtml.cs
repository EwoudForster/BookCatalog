using BookCatalog.DAL;
using BookCatalog.DAL.Repositories;
using BookCatalog.DAL.Repositories.Generic.Async;
using BookCatalog.DAL.Services.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.Web.Pages.Publishers
{
    [Authorize(Roles = "Administrator")]
    public class FormModel : PageModel
    {
        private readonly IBookRepository _bookRepository;
        private readonly IPublisherRepository _publisherRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly ILogger<FormModel> _logger;


        public FormModel(IBookRepository bookRepository, IPublisherRepository publisherRepository, IAuthorRepository authorRepository, ILogger<FormModel> logger)
        {
            try
            {
                _logger = logger ?? throw new ArgumentNullException(nameof(logger), LoggingStrings.ErrorNullArgument("Logger"));
                _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository), LoggingStrings.ErrorNullArgument("BookRepository"));
                _publisherRepository = publisherRepository ?? throw new ArgumentNullException(nameof(publisherRepository), LoggingStrings.ErrorNullArgument("publisherRepository"));
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
        public Publisher Publisher { get; set; }

        public async Task OnGet(Guid PublisherId)
        {
            try
            {
                Publisher = await _publisherRepository.GetById(PublisherId);
                if (Publisher == null)
                {
                    Publisher = new Publisher();
                    IsEdit = false;
                    ViewData["Title"] = "New Publisher";
                }
                else
                {
                   IsEdit = true;
                    ViewData["Title"] = $"Editing {Publisher.Name}";
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
                    await _publisherRepository.Update(Publisher);
                }
                else
                {
                    await _publisherRepository.Add(Publisher);
                }
                return Redirect($"/Genres/Details/{Publisher.Id}");
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
