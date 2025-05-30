namespace BookCatalog.DAL.DTO;


public class ReviewDTO
{
    public DateTime CreatedAt { get; set; }
    public Guid Id { get; set; }
    public DateTime UpdatedAt { get; set; }
    public UserDTOShort User { get; set; }
    public Guid UserId { get; set; }
    public BookDTO Book { get; set; }
    public Guid BookId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int Rating { get; set; }
}
