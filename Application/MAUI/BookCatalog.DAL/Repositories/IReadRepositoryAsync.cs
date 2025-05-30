using BookCatalog.DAL.Models;
using System.Linq.Expressions;

namespace BookCatalog.DAL.Repositories;


    // seperating the read and write operations in two interfaces
    // SRP - Single Responsibility Principle
    // output is covariant
    public interface IReadRepositoryAsync<T> where T : IEntity
    {
        Task<IEnumerable<T>> GetAll(bool sortCreatedAt = false);
        Task<T> GetById(Guid id);
    }

