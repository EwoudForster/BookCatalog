﻿namespace BookCatalog.DAL.Repositories.nonasync
{

    // Bringing them togheter
    public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T> where T : IEntity, new()
    {
    }
}
