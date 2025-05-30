using AutoMapper;
using BookCatalog.DAL;
using BookCatalog.DAL.DTO;
using BookCatalog.DAL.Logging;
using BookCatalog.DAL.Models;
using BookCatalog.DAL.Repositories;
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
        private readonly IMapper _mapper;



        public FormModel(IBookRepository bookRepository, IGenreRepository genreRepository, IAuthorRepository authorRepository, IMapper mapper, ILogger<FormModel> logger)
        {
            try
            {
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), LoggingStrings.ErrorNullArgument("Mapper"));
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
        public AuthorDTO Author { get; set; }

        public async Task OnGet(Guid AuthorId)
        {
            try
            {
                Author = _mapper.Map<AuthorDTO>( await _authorRepository.GetById(AuthorId));
                if (Author == null)
                {
                    Author = new AuthorDTO();
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
                    await _authorRepository.Update(_mapper.Map<Author>(Author));
                }
                else
                {
                    await _authorRepository.Add(_mapper.Map<Author>(Author));
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
