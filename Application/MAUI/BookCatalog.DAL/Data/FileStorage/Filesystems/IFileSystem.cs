
using BookCatalog.DAL.Models;

namespace BookCatalog.DAL.FileStorage;
    public interface IFileSystem<T> where T : IEntity
    {
        string FilePath(string? fileName);
        Task<IEnumerable<T>> Read();
        Task Save(IEnumerable<T> input);
    }
