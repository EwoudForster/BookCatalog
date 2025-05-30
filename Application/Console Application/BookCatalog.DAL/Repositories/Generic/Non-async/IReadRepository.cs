namespace BookCatalog.DAL.Repositories.nonasync
{

    // seperating the read and write operations in two interfaces
    // SRP - Single Responsibility Principle
    // output is covariant
    public interface IReadRepository<out T> where T : IEntity
    {
        IEnumerable<T> GetAll();
        T GetById(Guid id);
    }
}
