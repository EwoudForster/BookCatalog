using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;
using BookCatalog.DAL.Repositories;
using BookCatalog.DAL.Services.Logging;
using BookCatalog.DAL;
using AutoMapper;

namespace BookCatalog.ApiService
{
    [Authorize]
    [Route("api/[controller]")]
    public class PublishersController : ControllerBase
    {
        // making a private readonly field of the PublisherRepository
        private readonly IPublisherRepository _publisherRepository;

        // making a private readonly field of the logger
        private readonly ILogger<PublishersController> _logger;



        // creating a constructor for the PublishersController with dependency injection parameters for the PublisherRepository and the logger
        public PublishersController(IPublisherRepository publisherRepository, ILogger<PublishersController> logger)
        {
            try
            {
                // setting the private readonly fields to the dependency injection parameters with a null check
                _publisherRepository = publisherRepository ?? throw new ArgumentNullException(nameof(publisherRepository), LoggingStrings.ErrorNullArgument(nameof(publisherRepository)));
                _logger = logger ?? throw new ArgumentNullException(nameof(logger), LoggingStrings.ErrorNullArgument(nameof(logger)));


            }
            catch (Exception)
            {
                if (_logger != null)
                {
                    // logging that there was an error creating the PublishersController if the logger is not null
                    _logger.LogError(LoggingStrings.ErrorGeneralMethod($"creating {nameof(PublishersController)}"));
                }
                throw;
            }



        }

        // GET: api/Publishers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Publisher>>> GetPublishers()
        {
            // returning all the publishers from the publisher repository
            try
            {
                // returning all the publishers from the publisher repository
                return Ok(await _publisherRepository.GetAll(b => b.Books));
            }
            catch (Exception ex)
            {
                // logging an error if there is an exception
                _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("getting all publishers (API)"));
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        // GET: api/Publishers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Publisher>> GetPublisher(Guid id)
        {
            try
            {
                // getting a publisher by id
                var publisher = await _publisherRepository.GetById(id, b=> b.Books);

                // if the publisher is null, return a 404 not found
                if (publisher == null)
                {
                    return NotFound();
                }

                // return the publisher with a 200 OK
                return Ok(publisher);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("getting publisher (API)", null, id));
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        // PUT: api/Publishers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutPublisher(Publisher publisher)
        {
            try
            {
                // updating the publisher
                await _publisherRepository.Update(publisher);

                // returning no content if the publisher is updated
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                // logging an error if there is an exception
                _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("updating publisher (API)", null, publisher.Id));
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        // POST: api/Publishers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Publisher>> PostPublisher(Publisher publisher)
        {
            try
            {
                // adding a publisher
                await _publisherRepository.Add(publisher);
                return Ok(publisher);
            }
            catch (InvalidOperationException ex)
            {
                // logging an error if the publisher is not found
                _logger.LogError(ex, LoggingStrings.ErrorAlreadyExists(nameof(Publisher), publisher.Id));
                return BadRequest("Already exists");
            }
            catch (Exception ex)
            {
                // logging an error if there is an exception
                _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("Posting a publisher (API)", null, publisher.Id));
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        // DELETE: api/Publishers/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeletePublisher(Guid id)
        {
            try
            {
                // deleting a publisher
                await _publisherRepository.Delete(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                // logging an error if the publisher is not found
                _logger.LogError(ex, LoggingStrings.ErrorDoesNotExists(nameof(Publisher), id));
                return NotFound("Doesn't exist");
            }
            catch (Exception ex)
            {
                // logging an error if there is an exception
                _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("deleting publisher (API)", null, id));
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }

        }
    }
}
