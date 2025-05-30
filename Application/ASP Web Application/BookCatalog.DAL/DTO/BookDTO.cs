
namespace BookCatalog.DAL.DTO
{
    public class BookDTO
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public DateTimeOffset LastUpdated { get; set; }
        public string Title { get; set; }
        public List<AuthorDTO> Authors { get; set; } = new();
        public PublisherDTO Publisher { get; set; } = new();
        public Guid PublisherId { get; set; }
        public List<GenreDTO> Genres { get; set; } = new();
        public int PublicationYear { get; set; }
        public bool IsAvailable { get; set; }
        public string ImgUrl { get; set; }
        public string ISBN { get; set; }
        public int PageCount { get; set; }
        public decimal Price { get; set; }
        
    }
}
