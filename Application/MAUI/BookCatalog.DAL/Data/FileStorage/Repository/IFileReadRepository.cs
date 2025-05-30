using BookCatalog.DAL.Models;

namespace BookCatalog.DAL.FileStorage;

    // seperating the read and write operations in two interfaces
    // SRP - Single Responsibility Principle
    // output is covariant
    public interface IFileReadRepository<T> where T : IEntity
    {
        Task<IEnumerable<T>> GetAll(bool Sort = false);
        Task<T> GetById(Guid id);
    }

