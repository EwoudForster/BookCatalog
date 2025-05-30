using BookCatalog.DAL.Models.CalculatedValueModel;
using BookCatalog.DAL.Repositories.Generic.Async;

namespace BookCatalog.DAL.Repositories
{
    public interface IAuthorRepository : IRepositoryAsync<Author>
    {
        Task<IEnumerable<AuthorCalculated>> GetAllWithBookStats();
        Task<Dictionary<StatisticsOptions, object>> GetStatisticsForBooksPerAuthor(Guid AuthorId, BookByStatsOptions option = BookByStatsOptions.Price);
    }
}