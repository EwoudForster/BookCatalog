namespace BookCatalog.Models;

public interface IEntity
{
    DateTime CreatedAt { get; set; }
    Guid Id { get; set; }
    DateTime UpdatedAt { get; set; }
}