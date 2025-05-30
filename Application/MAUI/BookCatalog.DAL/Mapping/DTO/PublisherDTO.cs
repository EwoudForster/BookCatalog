namespace BookCatalog.DAL.DTO;

public class PublisherDTO
{
    public DateTime CreatedAt { get; set; }
    public Guid Id { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Name { get; set; }
    public virtual ICollection<BookDTO>? Books { get; set; }

}
