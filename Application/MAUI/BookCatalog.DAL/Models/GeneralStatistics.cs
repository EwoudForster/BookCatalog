namespace BookCatalog.DAL.Models;

    public class GeneralStatistics : EntityBase
    {
        public int TotalBooks { get; set; }
        public int TotalGenres { get; set; }
        public int TotalAuthors { get; set; }
        public int TotalPublishers { get; set; }
        public double MaxPrice { get; set; }
        public double MinPrice { get; set; }
        public double AveragePrice { get; set; }
    }
