﻿namespace BookCatalog.DAL
{
    public interface IEntity
    {
        Guid Id { get; set; }
        DateTimeOffset CreationDate { get; set; }
        DateTimeOffset LastUpdated { get; set; }
        string ToString();
    }
}
