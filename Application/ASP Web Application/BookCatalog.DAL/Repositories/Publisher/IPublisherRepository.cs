
using BookCatalog.DAL.Models.CalculatedValueModel;
using BookCatalog.DAL.Repositories.Generic.Async;

namespace BookCatalog.DAL.Repositories
{
    public interface IPublisherRepository : IRepositoryAsync<Publisher>
    {
        Task<Dictionary<StatisticsOptions, object>> GetStatisticsForBooksPerPublisher(Guid PublisherId, BookByStatsOptions option = BookByStatsOptions.Price);
        Task<IEnumerable<PublisherCalculated>> GetAllWithBookStats();
    }
}