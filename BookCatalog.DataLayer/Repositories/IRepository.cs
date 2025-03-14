namespace BookCatalog.DataLayer.Repositories
{
    public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T> where T : IEntity, new()
    {

    }
}
