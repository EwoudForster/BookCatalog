
using BookCatalog.DAL.Models;
using BookCatalog.DAL.Models.CalculatedValueModel;

namespace BookCatalog.DAL.Repositories
{
    public interface IGenreRepository : IRepository<Genre>
    {
        Task<IEnumerable<GenreCalculated>> GetAllWithBookStats();
        Task<Dictionary<StatisticsOptions, object>> GetStatisticsForBooksPerGenre(Guid GenreId, BookByStatsOptions option = BookByStatsOptions.Price);
    }
}