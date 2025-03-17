namespace BookCatalog.DataLayer.Repositories
{

    // seperating the read and write operations in two interfaces
    // SRP - Single Responsibility Principle
    // input is contravarient, so we can use the interface with a more specific type
    public interface IWriteRepository<in T> where T : IEntity, new()
    {
        void Add(T item);
        void Update(T item);
        void Delete(Guid id);
        void Save();
    }
}
