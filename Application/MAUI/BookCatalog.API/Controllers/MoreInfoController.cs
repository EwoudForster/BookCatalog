using AutoMapper;
using BookCatalog.DAL.DTO;
using BookCatalog.DAL.Logging;
using BookCatalog.DAL.Models;
using BookCatalog.DAL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.API;

[Authorize]
[Route("api/[controller]")]
public class MoreInfoController : ControllerBase
{
    // making a private readonly field of the MoreInfoRepository
    private readonly IMoreInfoRepository _moreInfoRepository;

    // making a private readonly field of the logger
    private readonly ILogger<MoreInfoController> _logger;
    private readonly IMapper _mapper;


    // creating a constructor for the MoreInfoController with dependency injection parameters for the MoreInfoRepository and the logger
    public MoreInfoController(IMoreInfoRepository moreInfoRepository, ILogger<MoreInfoController> logger, IMapper mapper)
    {
        try
        {
            // setting the private readonly fields to the dependency injection parameters with a null check
            _moreInfoRepository = moreInfoRepository ?? throw new ArgumentNullException(nameof(moreInfoRepository), LoggingStrings.ErrorNullArgument(nameof(moreInfoRepository)));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), LoggingStrings.ErrorNullArgument(nameof(logger)));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), LoggingStrings.ErrorNullArgument(nameof(mapper)));

        }
        catch (Exception)
        {
            if (_logger != null)
            {
                // logging that there was an error creating the MoreInfoController if the logger is not null
                _logger.LogError(LoggingStrings.ErrorGeneralMethod($"creating {nameof(MoreInfoController)}"));
            }
            throw;
        }

    }

    // GET: api/MoreInfo
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<MoreInfoDTO>>> GetMoreInfo()
    {
        // returning all the moreInfos from the moreInfo repository
        try
        {
            // returning all the moreInfos from the moreInfo repository
            return Ok(_mapper.Map<List<MoreInfoDTO>>(await _moreInfoRepository.GetAll()));
        }
        catch (Exception ex)
        {
            // logging an error if there is an exception
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("getting all moreInfos (API)"));
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }
    }

    // GET: api/MoreInfo/5
    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<MoreInfoDTO>> GetMoreInfo(Guid id)
    {
        try
        {
            // getting a moreInfo by id
            var moreInfo = _mapper.Map<MoreInfoDTO>(await _moreInfoRepository.GetById(id));

            // if the moreInfo is null, return a 404 not found
            if (moreInfo == null)
            {
                return NotFound();
            }

            // return the moreInfo with a 200 OK
            return Ok(moreInfo);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("getting moreInfo (API)", null, id));
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }
    }

    // PUT: api/MoreInfo/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> PutMoreInfo([FromBody] MoreInfoDTO moreInfo)
    {
        try
        {
            // updating the moreInfo
            await _moreInfoRepository.Update(_mapper.Map<MoreInfo>(moreInfo));

            // returning no content if the moreInfo is updated
            return NoContent();
        }
        catch (DbUpdateConcurrencyException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            // logging an error if there is an exception
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("updating moreInfo (API)", null, moreInfo.Id));
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }
    }

    // POST: api/MoreInfo
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<MoreInfoDTO>> PostMoreInfo([FromBody] MoreInfoDTO moreInfo)
    {
        try
        {
            // adding a moreInfo
            await _moreInfoRepository.Add(_mapper.Map<MoreInfo>(moreInfo));
            return Ok(moreInfo);
        }
        catch (InvalidOperationException ex)
        {
            // logging an error if the moreInfo is not found
            _logger.LogError(ex, LoggingStrings.ErrorAlreadyExists(nameof(MoreInfo), moreInfo.Id));
            return BadRequest("Already exists");
        }
        catch (Exception ex)
        {
            // logging an error if there is an exception
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("Posting a moreInfo (API)", null, moreInfo.Id));
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }
    }

    // DELETE: api/MoreInfo/5
    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteMoreInfo(Guid id)
    {
        try
        {
            // deleting a moreInfo
            await _moreInfoRepository.Delete(id);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            // logging an error if the moreInfo is not found
            _logger.LogError(ex, LoggingStrings.ErrorDoesNotExists(nameof(MoreInfo), id));
            return NotFound("Doesn't exist");
        }
        catch (Exception ex)
        {
            // logging an error if there is an exception
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("deleting moreInfo (API)", null, id));
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }

    }
}
