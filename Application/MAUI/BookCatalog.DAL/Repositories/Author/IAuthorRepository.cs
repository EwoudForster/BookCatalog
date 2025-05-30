using BookCatalog.DAL.Models;
using BookCatalog.DAL.Models.CalculatedValueModel;

namespace BookCatalog.DAL.Repositories
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<IEnumerable<AuthorCalculated>> GetAllWithBookStats();
        Task<Dictionary<StatisticsOptions, object>> GetStatisticsForBooksPerAuthor(Guid AuthorId, BookByStatsOptions option = BookByStatsOptions.Price);
    }
}