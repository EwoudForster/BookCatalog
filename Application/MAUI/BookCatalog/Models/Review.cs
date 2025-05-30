namespace BookCatalog.Models;

public class Review : EntityBase
{
    public virtual User User { get; set; }
    public Guid UserId { get; set; }
    public virtual Book Book { get; set; }
    public Guid BookId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int Rating { get; set; }
}
