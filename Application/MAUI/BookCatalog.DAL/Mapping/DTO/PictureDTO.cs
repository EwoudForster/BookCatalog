namespace BookCatalog.DAL.DTO;

public class PictureDTO
{
    public DateTime CreatedAt { get; set; }
    public Guid Id { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string ImgUrl { get; set; }
    public Guid BookId { get; set; }
    public virtual BookDTO? Book { get; set; }
}
