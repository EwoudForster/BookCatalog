using AutoMapper;
using BookCatalog.DAL.DTO;
using BookCatalog.DAL.Logging;
using BookCatalog.DAL.Mapping.DTO;
using BookCatalog.DAL.Models;
using BookCatalog.DAL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalog.API;

[Authorize]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<OrdersController> _logger;
    private readonly UserManager<User> _userManager;

    private readonly IMapper _mapper;

    public OrdersController(UserManager<User> userManager, IOrderRepository orderRepository, ILogger<OrdersController> logger, IMapper mapper)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager), LoggingStrings.ErrorNullArgument(nameof(userManager)));

        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    [Authorize(Roles = "Administrator")]

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders()
    {
        try
        {
            var orders = await _orderRepository.GetAll();
            return Ok(_mapper.Map<List<OrderDTO>>(orders));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving orders.");
            return StatusCode(500, "An unexpected error occurred.");
        }
    }
    [Authorize(Roles = "Administrator")]

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDTO>> GetOrder(Guid id)
    {
        try
        {
            var order = await _orderRepository.GetById(id);
            if (order == null) return NotFound();

            return Ok(_mapper.Map<OrderDTO>(order));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving order with ID {OrderId}.", id);
            return StatusCode(500, "An unexpected error occurred.");
        }
    }

    [HttpPost]
    public async Task<ActionResult<OrderDTO>> PostOrder([FromBody] CreateOrderDTO createDto)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(createDto.Email);
            var order = new Order
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                PersonId = user.Id,
                OrderItems = createDto.OrderItems.Select(oi => new OrderItem
                {
                    BookId = oi.BookId,
                    Amount = oi.Amount
                }).ToList()
            };

            await _orderRepository.Add(order);
            return Ok(_mapper.Map<OrderDTO>(order));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating order.");
            return StatusCode(500, "An unexpected error occurred.");
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteOrder(Guid id)
    {
        try
        {
            await _orderRepository.Delete(id);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "Order not found with ID {OrderId}.", id);
            return NotFound("Order not found.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting order.");
            return StatusCode(500, "An unexpected error occurred.");
        }
    }
}
