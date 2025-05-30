using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using BookCatalog.DAL.Repositories;
using AutoMapper;
using BookCatalog.DAL.DTO;
using BookCatalog.DAL.Models;
using Microsoft.AspNetCore.Identity;
using BookCatalog.DAL.Logging;

namespace BookCatalog.API;

[Authorize]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    // making a private readonly field of the UsersRepository
    private readonly UserManager<User> _userManager;

    // making a private readonly field of the logger
    private readonly ILogger<UsersController> _logger;
    private readonly IMapper _mapper;


    // creating a constructor for the UsersController with dependency injection parameters for the UsersRepository and the logger
    public UsersController(UserManager<User> userManager, ILogger<UsersController> logger, IMapper mapper)
    {
        try
        {
            // setting the private readonly fields to the dependency injection parameters with a null check
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager), LoggingStrings.ErrorNullArgument(nameof(userManager)));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), LoggingStrings.ErrorNullArgument(nameof(logger)));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), LoggingStrings.ErrorNullArgument(nameof(mapper)));

        }
        catch (Exception)
        {
            if (_logger != null)
            {
                // logging that there was an error creating the UsersController if the logger is not null
                _logger.LogError(LoggingStrings.ErrorGeneralMethod($"creating {nameof(UsersController)}"));
            }
            throw;
        }

    }

    // GET: api/Users
    [HttpGet]
   [Authorize]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        // returning all the users from the user repository
        try
        {
            var users = await _userManager.Users.ToListAsync();
            // returning all the users from the user repository
            return Ok(_mapper.Map<List<UserDTO>>(users));
        }
        catch (Exception ex)
        {
            // logging an error if there is an exception
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("getting all users (API)"));
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }
    }

    // GET: api/Users/5
    [HttpGet("{search}")]
   [Authorize]
    public async Task<ActionResult<User>> GetUserByIdOrEmail(string search)
    {
        try
        {
            User? user = null;

            if (Guid.TryParse(search, out Guid Id))
            {
                // getting a user by id
                user = await _userManager.FindByIdAsync(Id.ToString());
            }

            else
            {
                // getting a user by id
                user = await _userManager.FindByEmailAsync(search);

            }
            // if the user is null, return a 404 not found
            if (user == null)
            {
                _logger.LogInformation(LoggingStrings.WarningNoEntitiesFoundQuery("users", search));
                return NotFound();
            }

            // return the user with a 200 OK
            return Ok(_mapper.Map<UserDTO>(user));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("getting user (API)"));
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }
    }

    // PUT: api/Users/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut]
   [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> PutUsers([FromBody] UserDTO user)
    {
        try
        {
            // updating the user
            await _userManager.UpdateAsync(_mapper.Map<User>(user));

            // returning no content if the user is updated
            return NoContent();
        }
        catch (DbUpdateConcurrencyException)
        {
            _logger.LogInformation(LoggingStrings.WarningNoEntitiyIdFound("users", user.Id));
            return NotFound();
        }
        catch (Exception ex)
        {
            // logging an error if there is an exception
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("updating user (API)", null, user.Id));
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }
    }

    // POST: api/Users
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
   [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<UserDTO>> PostUser([FromBody] UserDTO user)
    {
        try
        {
            // adding a user
            await _userManager.CreateAsync(_mapper.Map<User>(user));
            return Ok(user);
        }
        catch (InvalidOperationException ex)
        {
            // logging an error if the user is not found
            _logger.LogError(ex, LoggingStrings.ErrorAlreadyExists(nameof(User), user.Id));
            return BadRequest("Already exists");
        }
        catch (Exception ex)
        {
            // logging an error if there is an exception
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("Posting a user (API)", null, user.Id));
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }
    }

    // DELETE: api/Users/5
    [HttpDelete("{id}")]
   [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                throw new InvalidOperationException();
            }
            // deleting a user
            await _userManager.DeleteAsync(user);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            // logging an error if the user is not found
            _logger.LogError(ex, LoggingStrings.ErrorDoesNotExists(nameof(User), id));
            return NotFound("Doesn't exist");
        }
        catch (Exception ex)
        {
            // logging an error if there is an exception
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("deleting user (API)", null, id));
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }

    }
}
