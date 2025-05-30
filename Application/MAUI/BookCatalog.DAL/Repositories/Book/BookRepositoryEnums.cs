namespace BookCatalog.DAL.Repositories
{
        public enum BookByOptions
        {
            Genre,
            Year,
            Price,
            Publisher,
            Page,
            Title,
            IsAvailable,
            Author
        }

        public enum StatisticsOptions
        {
            StatisticsType,
            Average,
            Max,
            Min,
            TotalBooks,
        }

        public enum BookByStatsOptions
        {
            Year,
            Price,
            Page,
        }
}
