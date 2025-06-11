using BookCatalog.DAL.Models;

namespace BookCatalog.DAL.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<PaginatedList<Order>> GetFilteredOrdersAsync(string sortOrder, string currentFilter, string searchString, DateTime? dateFrom, DateTime? dateTo, int? pageIndex, int pageSize = 10);
    }
}