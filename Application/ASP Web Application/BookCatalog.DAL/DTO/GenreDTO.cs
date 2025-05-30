
namespace BookCatalog.DAL.DTO
{
    public class GenreDTO
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public DateTimeOffset LastUpdated { get; set; }
        public string Name { get; set; }
        public List<BookDTO> Books { get; set; } = new();
    }
}
