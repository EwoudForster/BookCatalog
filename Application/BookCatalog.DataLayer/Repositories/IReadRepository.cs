namespace BookCatalog.DataLayer.Repositories
{
    public interface IReadRepository<out T> where T : IEntity
    {
        IEnumerable<T> GetAll();
        T GetId(Guid id);
    }
}
