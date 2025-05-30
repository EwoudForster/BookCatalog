using BookCatalog.DAL.Models;

namespace BookCatalog.DAL.Repositories;

// seperating the read and write operations in two interfaces
// SRP - Single Responsibility Principle
// input is contravarient, so we can use the interface with a more specific type
public interface IWriteRepositoryAsync<in T> where T : IEntity, new()
{
    Task Add(T item);
    Task Update(T item);
    Task Delete(Guid id);
    Task Save();
}

