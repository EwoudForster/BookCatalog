
using BookCatalog.DAL.Models;

namespace BookCatalog.DAL.FileStorage;

public interface IFileRepository<T> : IFileReadRepository<T>, IFileWriteRepository<T> where T : IEntity
{
}