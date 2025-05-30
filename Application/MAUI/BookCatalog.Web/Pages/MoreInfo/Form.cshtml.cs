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

namespace BookCatalog.Web.Pages.MoreInfos
{
    [Authorize(Roles = "Administrator")]
    public class FormModel : PageModel
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMoreInfoRepository _moreInfoRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly ILogger<FormModel> _logger;
        private readonly IMapper _mapper;



        public FormModel(IBookRepository bookRepository, IMoreInfoRepository MoreInfoRepository, IAuthorRepository authorRepository, IMapper mapper, ILogger<FormModel> logger)
        {
            try
            {
                _logger = logger ?? throw new ArgumentNullException(nameof(logger), LoggingStrings.ErrorNullArgument("Logger"));
                _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository), LoggingStrings.ErrorNullArgument("BookRepository"));
                _moreInfoRepository = MoreInfoRepository ?? throw new ArgumentNullException(nameof(MoreInfoRepository), LoggingStrings.ErrorNullArgument("MoreInfoRepository"));
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
        public MoreInfoDTO MoreInfo { get; set; }

        public async Task OnGet(Guid MoreInfoId)
        {
            try
            {
                MoreInfo = _mapper.Map<MoreInfoDTO>(await _moreInfoRepository.GetById(MoreInfoId));
                if (MoreInfo == null)
                {
                    MoreInfo = new MoreInfoDTO();
                    IsEdit = false;
                    ViewData["Title"] = "New MoreInfo";
                }
                else
                {
                   IsEdit = true;
                    ViewData["Title"] = $"Editing {MoreInfo.Name}";
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
                    await _moreInfoRepository.Update(_mapper.Map<MoreInfo>(MoreInfo));
                }
                else
                {
                    await _moreInfoRepository.Add(_mapper.Map<MoreInfo>(MoreInfo));
                }
                return Redirect($"/MoreInfos/Details/{MoreInfo.Id}");
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
