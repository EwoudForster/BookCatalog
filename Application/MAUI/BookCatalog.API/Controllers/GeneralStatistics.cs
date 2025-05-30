using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using BookCatalog.DAL.Logging;
using BookCatalog.DAL.Repositories;
using BookCatalog.DAL.DTO;
using BookCatalog.DAL.Models;


namespace BookCatalogs.API;

[Route("api/[controller]")]
public class GeneralStatisticsController : ControllerBase
{
    // making a private readonly field of the LogRepository
    private readonly IGeneralStatisticsRepository _generalStatisticsRepository;

    // making a private readonly field of the logger
    private readonly ILogger<GeneralStatisticsController> _logger;
    private readonly IMapper _mapper;


    // creating a constructor for the LogController with dependency injection parameters for the LogRepository and the logger
    public GeneralStatisticsController(IGeneralStatisticsRepository generalStatisticsRepository, ILogger<GeneralStatisticsController> logger, IMapper mapper)
    {
        try
        {
            // setting the private readonly fields to the dependency injection parameters with a null check
            _generalStatisticsRepository = generalStatisticsRepository ?? throw new ArgumentNullException(nameof(generalStatisticsRepository), LoggingStrings.ErrorNullArgument(nameof(generalStatisticsRepository)));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), LoggingStrings.ErrorNullArgument(nameof(logger)));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), LoggingStrings.ErrorNullArgument(nameof(mapper)));

        }
        catch (Exception)
        {
            if (_logger != null)
            {
                // logging that there was an error creating the LogController if the logger is not null
                _logger.LogError(LoggingStrings.ErrorGeneralMethod($"creating {nameof(GeneralStatisticsController)}"));
            }
            throw;
        }

    }

    // GET: api/Log
    [HttpGet]
    public async Task<ActionResult<GeneralStatisticsDTO>> GetGeneralStatistics()
    {
        // returning all the logss from the generalStatistics repository
        try
        {
            // returning all the logss from the generalStatistics repository
            return Ok(_mapper.Map<GeneralStatisticsDTO>(await _generalStatisticsRepository.GetGeneralStatistics()));
        }
        catch (Exception ex)
        {
            // logging an error if there is an exception
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("getting all statistics (API)"));
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }
    }
}
