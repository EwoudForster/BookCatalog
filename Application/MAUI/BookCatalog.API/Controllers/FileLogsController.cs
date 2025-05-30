using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using BookCatalog.DAL.Repositories;
using AutoMapper;
using BookCatalog.DAL.DTO;
using BookCatalog.DAL.Logging;
using BookCatalog.DAL.FileStorage;


namespace BookCatalog.API;

[Authorize(Roles = "Administrator")]
[Route("api/[controller]")]
public class FileLogsController : ControllerBase
{
    // making a private readonly field of the LogRepository
    private readonly IFileRepository<LoggingEntry> _logFileRepository;

    // making a private readonly field of the logger
    private readonly ILogger<FileLogsController> _logger;
    private readonly IMapper _mapper;


    // creating a constructor for the LogsController with dependency injection parameters for the LogRepository and the logger
    public FileLogsController(IFileRepository<LoggingEntry> logRepository, ILogger<FileLogsController> logger, IMapper mapper)
    {
        try
        {
            // setting the private readonly fields to the dependency injection parameters with a null check
            _logFileRepository = logRepository ?? throw new ArgumentNullException(nameof(logRepository), LoggingStrings.ErrorNullArgument(nameof(logRepository)));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), LoggingStrings.ErrorNullArgument(nameof(logger)));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), LoggingStrings.ErrorNullArgument(nameof(mapper)));

        }
        catch (Exception)
        {
            if (_logger != null)
            {
                // logging that there was an error creating the LogsController if the logger is not null
                _logger.LogError(LoggingStrings.ErrorGeneralMethod($"creating {nameof(FileLogsController)}"));
            }
            throw;
        }

    }

    // GET: api/Logs
    [HttpGet]
   [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<IEnumerable<LoggingEntryDTO>>> GetLogs()
    {
        // returning all the logs from the log repository
        try
        {
            // returning all the logs from the log repository
            return Ok(_mapper.Map<List<LoggingEntryDTO>>(await _logFileRepository.GetAll(true)));
        }
        catch (Exception ex)
        {
            // logging an error if there is an exception
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("getting all logs (API)"));
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }
    }

    // GET: api/Logs/5
    [HttpGet("{id}")]
   [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<LoggingEntryDTO>> GetLog(Guid id)
    {
        try
        {
            // getting a log by id
            var log = _mapper.Map<LoggingEntryDTO>(await _logFileRepository.GetById(id));

            // if the log is null, return a 404 not found
            if (log == null)
            {
                return NotFound();
            }

            // return the log with a 200 OK
            return Ok(log);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("getting log (API)", null, id));
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }
    }
}
