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
using System.Collections.Generic;

namespace BookCatalog.Web.Pages.Genres
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
                _logger = logger ?? throw new ArgumentNullException(nameof(logger), LoggingStrings.ErrorNullArgument("Logger"));
                _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository), LoggingStrings.ErrorNullArgument("BookRepository"));
                _genreRepository = genreRepository ?? throw new ArgumentNullException(nameof(genreRepository), LoggingStrings.ErrorNullArgument("GenreRepository"));
                _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository), LoggingStrings.ErrorNullArgument("AuthorRepository"));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), LoggingStrings.ErrorNullArgument(nameof(mapper)));

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
        public GenreDTO Genre { get; set; }

        public async Task OnGet(Guid GenreId)
        {
            try
            {
                Genre = _mapper.Map<GenreDTO>(await _genreRepository.GetById(GenreId));
                if (Genre == null)
                {
                    Genre = new GenreDTO();
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
                    await _genreRepository.Update(_mapper.Map<Genre>(Genre));
                }
                else
                {
                    await _genreRepository.Add(_mapper.Map<Genre>(Genre));
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
