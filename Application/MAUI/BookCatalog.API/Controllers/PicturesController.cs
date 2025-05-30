using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookCatalog.DAL.Repositories;
using AutoMapper;
using BookCatalog.DAL.DTO;
using BookCatalog.DAL.Models;
using BookCatalog.DAL.Logging;
using Microsoft.AspNetCore.Authorization;

namespace BookCatalog.API;

[Authorize]
[Route("api/[controller]")]
public class PicturesController : ControllerBase
{
    // making a private readonly field of the PicturesRepository
    private readonly IRepository<Picture> _pictureRepository;

    // making a private readonly field of the logger
    private readonly ILogger<PicturesController> _logger;
    private readonly IMapper _mapper;


    // creating a constructor for the PicturesController with dependency injection parameters for the PicturesRepository and the logger
    public PicturesController(IRepository<Picture> picturesRepository, ILogger<PicturesController> logger, IMapper mapper)
    {
        try
        {
            // setting the private readonly fields to the dependency injection parameters with a null check
            _pictureRepository = picturesRepository ?? throw new ArgumentNullException(nameof(picturesRepository), LoggingStrings.ErrorNullArgument(nameof(picturesRepository)));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), LoggingStrings.ErrorNullArgument(nameof(logger)));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), LoggingStrings.ErrorNullArgument(nameof(mapper)));

        }
        catch (Exception)
        {
            if (_logger != null)
            {
                // logging that there was an error creating the PicturesController if the logger is not null
                _logger.LogError(LoggingStrings.ErrorGeneralMethod($"creating {nameof(PicturesController)}"));
            }
            throw;
        }

    }

    // GET: api/Pictures
    [HttpGet]
   [Authorize]
    public async Task<ActionResult<IEnumerable<PictureDTO>>> GetPictures()
    {
        // returning all the picturess from the pictures repository
        try
        {
            // returning all the picturess from the pictures repository
            return Ok(_mapper.Map<List<PictureDTO>>(await _pictureRepository.GetAll()));
        }
        catch (Exception ex)
        {
            // logging an error if there is an exception
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("getting all picturess (API)"));
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }
    }

    // GET: api/Pictures/5
    [HttpGet("{id}")]
   [Authorize]
    public async Task<ActionResult<PictureDTO>> GetPictures(Guid id)
    {
        try
        {
            // getting a pictures by id
            var pictures = _mapper.Map<PictureDTO>(await _pictureRepository.GetById(id));

            // if the pictures is null, return a 404 not found
            if (pictures == null)
            {
                return NotFound();
            }

            // return the pictures with a 200 OK
            return Ok(pictures);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("getting picture (API)", null, id));
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }
    }

    // PUT: api/Pictures/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut]
   [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> PutPicture([FromBody] PictureDTO picture)
    {
        try
        {
            // updating the pictures
            await _pictureRepository.Update(_mapper.Map<Picture>(picture));

            // returning no content if the pictures is updated
            return NoContent();
        }
        catch (DbUpdateConcurrencyException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            // logging an error if there is an exception
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("updating picture (API)", null, picture.Id));
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }
    }

    // POST: api/Pictures
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
   [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<PictureDTO>> PostPictures([FromBody] PictureDTO pictures)
    {
        try
        {
            // adding a pictures
            await _pictureRepository.Add(_mapper.Map<Picture>(pictures));
            return Ok(pictures);
        }
        catch (InvalidOperationException ex)
        {
            // logging an error if the pictures is not found
            _logger.LogError(ex, LoggingStrings.ErrorAlreadyExists(nameof(Picture), pictures.Id));
            return BadRequest("Already exists");
        }
        catch (Exception ex)
        {
            // logging an error if there is an exception
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("Posting a picture (API)", null, pictures.Id));
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }
    }

    // DELETE: api/Pictures/5
    [HttpDelete("{id}")]
   [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeletePicture(Guid id)
    {
        try
        {
            // deleting a pictures
            await _pictureRepository.Delete(id);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            // logging an error if the pictures is not found
            _logger.LogError(ex, LoggingStrings.ErrorDoesNotExists(nameof(Picture), id));
            return NotFound("Doesn't exist");
        }
        catch (Exception ex)
        {
            // logging an error if there is an exception
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("deleting picture (API)", null, id));
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }

    }
}
