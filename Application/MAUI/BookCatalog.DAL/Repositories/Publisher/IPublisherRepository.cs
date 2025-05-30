
using BookCatalog.DAL.Models;
using BookCatalog.DAL.Models.CalculatedValueModel;

namespace BookCatalog.DAL.Repositories
{
    public interface IPublisherRepository : IRepository<Publisher>
    {
        Task<Dictionary<StatisticsOptions, object>> GetStatisticsForBooksPerPublisher(Guid PublisherId, BookByStatsOptions option = BookByStatsOptions.Price);
        Task<IEnumerable<PublisherCalculated>> GetAllWithBookStats();
    }
}