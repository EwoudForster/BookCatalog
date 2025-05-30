namespace BookCatalog.DAL.DTO;

public class PictureDTOShort
{
    public DateTime CreatedAt { get; set; }
    public Guid Id { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string ImgUrl { get; set; }
    public Guid BookId { get; set; }
}
