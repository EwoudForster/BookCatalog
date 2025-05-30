
using BookCatalog.DAL.Models;

namespace BookCatalog.DAL.FileStorage;

public interface ISerialize<T> where T : IEntity
{
    Task<List<T>> DeSerializer(Stream value);
    Task Serializer(List<T> items, Stream stream);
}