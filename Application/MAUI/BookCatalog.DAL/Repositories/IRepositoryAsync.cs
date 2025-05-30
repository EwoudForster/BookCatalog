using BookCatalog.DAL.Models;

namespace BookCatalog.DAL.Repositories;


    // Bringing them togheter
    public interface IRepository<T> : IReadRepositoryAsync<T>, IWriteRepositoryAsync<T> where T : IEntity, new()
    {
    }

