namespace BookCatalog.DataLayer.Repositories
{
    public interface IWriteRepository<in T> where T : IEntity, new()
    {
        void Add(T item);
        void Update(T item);
        void Delete(Guid id);
        void Save();
    }
}
