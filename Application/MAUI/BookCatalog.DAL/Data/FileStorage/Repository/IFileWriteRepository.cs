using BookCatalog.DAL.Models;

namespace BookCatalog.DAL.FileStorage;

// seperating the read and write operations in two interfaces
// SRP - Single Responsibility Principle
// input is contravarient, so we can use the interface with a more specific type
public interface IFileWriteRepository<in T> where T : IEntity
{
    Task Add(T item);
    Task Delete(Guid id);
    Task Save();
    Task Update(T item);
}
