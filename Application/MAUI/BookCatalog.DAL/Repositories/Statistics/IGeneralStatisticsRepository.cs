using BookCatalog.DAL.Models;

namespace BookCatalog.DAL.Repositories
{
    public interface IGeneralStatisticsRepository
    {
        Task<GeneralStatistics> GetGeneralStatistics();
    }
}