namespace BookCatalog.DAL.DTO;

public class BookDTO
{
    public DateTime CreatedAt { get; set; }
    public Guid Id { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Title { get; set; }
    public int PublicationYear { get; set; }
    public string ISBN { get; set; }
    public int PageCount { get; set; }
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }        
    public string? Description { get; set; }
    public ICollection<PictureDTOShort>? Pictures { get; set; }
    public ICollection<MoreInfoDTOShort>? MoreInfos { get; set; }
    public ICollection<ReviewDTOShort>? Reviews { get; set; }        
    public virtual PublisherDTOShort? Publisher { get; set; }
    public Guid PublisherId { get; set; }
    public double AverageRating => Reviews.Select(p => p.Rating).Average();
    public string FirstImageUrl => Pictures?.FirstOrDefault()?.ImgUrl ?? "https://cdn1.iconfinder.com/data/icons/business-company-1/500/image-1024.png";
    public virtual ICollection<AuthorDTOShort>? Authors { get; set; }

    public virtual ICollection<GenreDTOShort>? Genres { get; set; }

}
