
using BookCatalog.DAL.Models.CalculatedValueModel;
using BookCatalog.DAL.Repositories.Generic.Async;

namespace BookCatalog.DAL.Repositories
{
    public interface IGenreRepository : IRepositoryAsync<Genre>
    {
        Task<IEnumerable<GenreCalculated>> GetAllWithBookStats();
        Task<Dictionary<StatisticsOptions, object>> GetStatisticsForBooksPerGenre(Guid GenreId, BookByStatsOptions option = BookByStatsOptions.Price);
    }
}