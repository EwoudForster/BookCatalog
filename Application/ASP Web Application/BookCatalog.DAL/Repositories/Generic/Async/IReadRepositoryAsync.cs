using System.Linq.Expressions;

namespace BookCatalog.DAL.Repositories.Generic.Async
{

    // seperating the read and write operations in two interfaces
    // SRP - Single Responsibility Principle
    // output is covariant
    public interface IReadRepositoryAsync<T> where T : IEntity
    {
        Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includes);
        Task<T> GetById(Guid id, params Expression<Func<T, object>>[] includes);
    }
}
