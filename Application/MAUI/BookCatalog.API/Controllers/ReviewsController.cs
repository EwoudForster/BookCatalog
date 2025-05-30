using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using BookCatalog.DAL.Repositories;
using AutoMapper;
using BookCatalog.DAL.DTO;
using BookCatalog.DAL.Models;
using BookCatalog.DAL.Logging;

namespace BookCatalog.API;

[Authorize]
[Route("api/[controller]")]
public class ReviewsController : ControllerBase
{
    // making a private readonly field of the ReviewRepository
    private readonly IRepository<Review> _reviewRepository;

    // making a private readonly field of the logger
    private readonly ILogger<ReviewsController> _logger;
    private readonly IMapper _mapper;


    // creating a constructor for the ReviewController with dependency injection parameters for the ReviewRepository and the logger
    public ReviewsController(IRepository<Review> reviewRepository, ILogger<ReviewsController> logger, IMapper mapper)
    {
        try
        {
            // setting the private readonly fields to the dependency injection parameters with a null check
            _reviewRepository = reviewRepository ?? throw new ArgumentNullException(nameof(reviewRepository), LoggingStrings.ErrorNullArgument(nameof(reviewRepository)));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), LoggingStrings.ErrorNullArgument(nameof(logger)));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), LoggingStrings.ErrorNullArgument(nameof(mapper)));

        }
        catch (Exception)
        {
            if (_logger != null)
            {
                // logging that there was an error creating the ReviewController if the logger is not null
                _logger.LogError(LoggingStrings.ErrorGeneralMethod($"creating {nameof(ReviewsController)}"));
            }
            throw;
        }

    }

    // GET: api/Review
    [HttpGet]
   [Authorize]
    public async Task<ActionResult<IEnumerable<ReviewDTO>>> GetReviews()
    {
        // returning all the reviews from the review repository
        try
        {
            // returning all the reviews from the review repository
            return Ok(_mapper.Map<List<ReviewDTO>>(await _reviewRepository.GetAll()));
        }
        catch (Exception ex)
        {
            // logging an error if there is an exception
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("getting all reviews (API)"));
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }
    }

    // GET: api/Review/5
    [HttpGet("{id}")]
   [Authorize]
    public async Task<ActionResult<ReviewDTO>> GetReview(Guid id)
    {
        try
        {
            // getting a review by id
            var review = _mapper.Map<ReviewDTO>(await _reviewRepository.GetById(id));

            // if the review is null, return a 404 not found
            if (review == null)
            {
                return NotFound();
            }

            // return the review with a 200 OK
            return Ok(review);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("getting review (API)", null, id));
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }
    }

    // PUT: api/Review/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut]
   [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> PutReview([FromBody] ReviewDTO review)
    {
        try
        {
            // updating the review
            await _reviewRepository.Update(_mapper.Map<Review>(review));

            // returning no content if the review is updated
            return NoContent();
        }
        catch (DbUpdateConcurrencyException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            // logging an error if there is an exception
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("updating review (API)", null, review.Id));
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }
    }

    // POST: api/Review
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
   [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<ReviewDTO>> PostReview([FromBody]ReviewDTO review)
    {
        try
        {
            // adding a review
            await _reviewRepository.Add(_mapper.Map<Review>(review));
            return Ok(review);
        }
        catch (InvalidOperationException ex)
        {
            // logging an error if the review is not found
            _logger.LogError(ex, LoggingStrings.ErrorAlreadyExists(nameof(Review), review.Id));
            return BadRequest("Already exists");
        }
        catch (Exception ex)
        {
            // logging an error if there is an exception
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("Posting a review (API)", null, review.Id));
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }
    }

    // DELETE: api/Review/5
    [HttpDelete("{id}")]
   [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteReview(Guid id)
    {
        try
        {
            // deleting a review
            await _reviewRepository.Delete(id);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            // logging an error if the review is not found
            _logger.LogError(ex, LoggingStrings.ErrorDoesNotExists(nameof(Review), id));
            return NotFound("Doesn't exist");
        }
        catch (Exception ex)
        {
            // logging an error if there is an exception
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("deleting review (API)", null, id));
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }

    }
}
