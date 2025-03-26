using System.Linq.Expressions;

namespace BookCatalog.DataLayer.Repositories
{

    // Bringing them togheter
    public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T> where T : IEntity, new()
    {
    }
}
