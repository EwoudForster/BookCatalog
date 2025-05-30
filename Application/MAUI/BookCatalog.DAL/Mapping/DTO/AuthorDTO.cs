namespace BookCatalog.DAL.DTO;

public class AuthorDTO
{
    public DateTime CreatedAt { get; set; }
    public Guid Id { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual ICollection<BookDTO>? Books { get; set; }
}
