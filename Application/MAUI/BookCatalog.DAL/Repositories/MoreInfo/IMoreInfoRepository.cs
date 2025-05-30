using BookCatalog.DAL.Models;
using BookCatalog.DAL.Models.CalculatedValueModel;

namespace BookCatalog.DAL.Repositories
{
    public interface IMoreInfoRepository : IRepository<MoreInfo>
    {
        Task<IEnumerable<MoreInfoCalculated>> GetAllWithBookStats();
        Task<Dictionary<StatisticsOptions, object>> GetStatisticsForBooksPerMoreInfo(Guid MoreInfoId, BookByStatsOptions option = BookByStatsOptions.Price);
    }
}