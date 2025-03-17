using BookCatalog.DataLayer;

namespace BookCatalog.DataLayer
{
    public class Book : EntityBase
    {
        // Book properties
        public string Title { get; set; }
        public string Author { get; set; }
        public int PublicationYear { get; set; }
        public string Genre { get; set; }
        public string ISBN { get; set; }
        public string Publisher { get; set; }
        public int PageCount { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }

        // Book ToString method
        public override string ToString() => $"{base.ToString()}" +
            $"\n\tTitle: {Title}" +
            $"\n\tAuthor: {Author}" +
            $"\n\tPublication Year: {PublicationYear}" +
            $"\n\tGenre: {Genre}" +
            $"\n\tISBN: {ISBN}" +
            $"\n\tPublisher: {Publisher}"+
            $"\n\tAmount of Pages: {PageCount}"+
            $"\n\tPrice: {Price}"+
            $"\n\tAvailable: {IsAvailable}\n";
    }
}
