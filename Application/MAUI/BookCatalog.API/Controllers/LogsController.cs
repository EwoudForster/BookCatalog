using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using BookCatalog.DAL.Logging;
using BookCatalog.DAL.Repositories;
using BookCatalog.DAL.DTO;


namespace BookCatalogs.API;

[Authorize(Roles = "Administrator")]
[Route("api/[controller]")]
public class LogsController : ControllerBase
{
    // making a private readonly field of the LogRepository
    private readonly IRepository<LoggingEntry> _logsRepository;

    // making a private readonly field of the logger
    private readonly ILogger<LogsController> _logger;
    private readonly IMapper _mapper;


    // creating a constructor for the LogController with dependency injection parameters for the LogRepository and the logger
    public LogsController(IRepository<LoggingEntry> logsRepository, ILogger<LogsController> logger, IMapper mapper)
    {
        try
        {
            // setting the private readonly fields to the dependency injection parameters with a null check
            _logsRepository = logsRepository ?? throw new ArgumentNullException(nameof(logsRepository), LoggingStrings.ErrorNullArgument(nameof(logsRepository)));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), LoggingStrings.ErrorNullArgument(nameof(logger)));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), LoggingStrings.ErrorNullArgument(nameof(mapper)));

        }
        catch (Exception)
        {
            if (_logger != null)
            {
                // logging that there was an error creating the LogController if the logger is not null
                _logger.LogError(LoggingStrings.ErrorGeneralMethod($"creating {nameof(LogsController)}"));
            }
            throw;
        }

    }

    // GET: api/Log
    [HttpGet]
   [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<LoggingEntryDTO>>> GetLogs()
    {
        // returning all the logss from the logs repository
        try
        {
            // returning all the logss from the logs repository
            return Ok(_mapper.Map<List<LoggingEntryDTO>>(await _logsRepository.GetAll(true)));
        }
        catch (Exception ex)
        {
            // logging an error if there is an exception
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("getting all logss (API)"));
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }
    }

    // GET: api/Log/5
    [HttpGet("{id}")]
   [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<LoggingEntryDTO>> GetLog(Guid id)
    {
        try
        {
            // getting a logs by id
            var logs = _mapper.Map<LoggingEntryDTO>(await _logsRepository.GetById(id));

            // if the logs is null, return a 404 not found
            if (logs == null)
            {
                return NotFound();
            }

            // return the logs with a 200 OK
            return Ok(logs);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("getting logs (API)", null, id));
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }
    }
}
