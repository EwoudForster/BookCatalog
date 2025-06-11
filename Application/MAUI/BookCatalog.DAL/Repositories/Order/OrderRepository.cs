using BookCatalog.DAL.Data;
using BookCatalog.DAL.Logging;
using BookCatalog.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookCatalog.DAL.Repositories
{
    public class OrderRepository : GenericRepositoryAsync<Order>, IOrderRepository
    {

        public OrderRepository(BookCatalogDbContext context, ILogger<OrderRepository> logger) : base(context, logger)
        {
        }

        // Get all entities with count of books per MoreInfo
        public async Task<PaginatedList<Order>> GetFilteredOrdersAsync(
    string sortOrder,
    string currentFilter,
    string searchString,
    DateTime? dateFrom,
    DateTime? dateTo,
    int? pageIndex,
    int pageSize = 10)
        {
            try
            {

                if (searchString != null)
                {
                    pageIndex = 1;
                }
                else
                {
                    searchString = currentFilter;
                }


                IQueryable<Order> ordersQuery = _dbSet
                    .Include(o => o.Person)
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.Book)
                    .AsQueryable();


                // Logging that the search is in progress with a query
                _logger.LogInformation(LoggingStrings.InfoSearchingEntitiesQuery(_entityType.Name, searchString));
                // Search filter
                if (!string.IsNullOrEmpty(searchString))
                {
                    ordersQuery = ordersQuery.Where(o =>
                        o.Person.FirstName.Contains(searchString) ||
                        o.Person.LastName.Contains(searchString) ||
                        o.Person.Email.Contains(searchString) ||
                        o.Id.ToString().Contains(searchString));
                }

                // Date filters
                if (dateFrom.HasValue)
                {
                    ordersQuery = ordersQuery.Where(o => o.CreatedAt >= dateFrom.Value);
                }

                if (dateTo.HasValue)
                {
                    var endOfDay = dateTo.Value.Date.AddDays(1).AddTicks(-1);
                    ordersQuery = ordersQuery.Where(o => o.CreatedAt <= endOfDay);
                }

                // Sorting
                ordersQuery = sortOrder switch
                {
                    "id_desc" => ordersQuery.OrderByDescending(o => o.Id),
                    "customer" => ordersQuery.OrderBy(o => o.Person.LastName).ThenBy(o => o.Person.FirstName),
                    "customer_desc" => ordersQuery.OrderByDescending(o => o.Person.LastName).ThenByDescending(o => o.Person.FirstName),
                    "date" => ordersQuery.OrderBy(o => o.CreatedAt),
                    "date_desc" => ordersQuery.OrderByDescending(o => o.CreatedAt),
                    "total" => ordersQuery.OrderBy(o => o.OrderItems.Sum(oi => oi.Book.Price * oi.Amount)),
                    "total_desc" => ordersQuery.OrderByDescending(o => o.OrderItems.Sum(oi => oi.Book.Price * oi.Amount)),
                    _ => ordersQuery.OrderByDescending(o => o.CreatedAt)
                };

                int currentPage = pageIndex ?? 1;
                int totalItems = await ordersQuery.CountAsync();
                // logging that we have gotten the entity
                _logger.LogInformation(LoggingStrings.InfoRetreivingEntitiesQuery(_entityType.Name, searchString));
                List<Order> pagedOrders = await ordersQuery
                    .Skip((currentPage - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
                return new PaginatedList<Order>(pagedOrders, totalItems, currentPage, pageSize);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("GetFilteredOrdersAsync", _entityType.Name));
                throw;
            }
        }




    }
}
