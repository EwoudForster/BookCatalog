namespace BookCatalog.DAL.Repositories.Generic.Async
{

    // Bringing them togheter
    public interface IRepositoryAsync<T> : IReadRepositoryAsync<T>, IWriteRepositoryAsync<T> where T : IEntity, new()
    {
    }
}
