using BookCatalog.DataLayer;

namespace BookCatalog.Services
{
    public static class LINQBook
    {
        public static IEnumerable<Book> Search(this IEnumerable<Book> list, string query)
        {
            try
            {
                var refactoredQuery = query.ToLower();
                return list.Where(x => x.Title.ToLower().Contains(refactoredQuery) || x.Publisher.ToLower().Contains(refactoredQuery) || x.Author.ToLower().Contains(refactoredQuery) || x.Genre.ToLower().Contains(refactoredQuery));
            }
            catch
            {
                return new List<Book>();
            }
        }

        public static IEnumerable<Book> OrderBookBy(this IEnumerable<Book> list, string? query = "")
        {
            var reformattedQuery = query;
            if (query != null)
            {
                reformattedQuery = query.ToLower();
            }
            switch (reformattedQuery)
            {
                case var a when reformattedQuery.Contains("year"):
                case var b when reformattedQuery.Contains("date"):
                case var c when reformattedQuery.Contains("publised"):
                    return list.OrderBy(e => e.PublicationYear);

                case var a when reformattedQuery.Contains("price"):
                case var b when reformattedQuery.Contains("cost"):
                case var c when reformattedQuery.Contains("money"): 
                    return list.OrderBy(e => e.Price);

                case var a when reformattedQuery.Contains("publisher"):
                case var b when reformattedQuery.Contains("company"):
                case var c when reformattedQuery.Contains("distributer"):
                case "publisher":
                    return list.OrderBy(e => e.Publisher);

                case var a when reformattedQuery.Contains("page"):
                case var b when reformattedQuery.Contains("count"):
                case var c when reformattedQuery.Contains("number"):
                    return list.OrderBy(e => e.PageCount);

                default:
                    return list.OrderBy(e => e.Title);
            }
        }
        public static IEnumerable<IGrouping<string, Book>> GroupByGenre(this IEnumerable<Book> list)
        {
            return list.GroupBy(x => x.Genre);
        }

        public static IEnumerable<IGrouping<int, Book>> GroupByPublicationYear(this IEnumerable<Book> list)
        {
            return list.GroupBy(x => x.PublicationYear);
        }

        public static (decimal averagePrice, int totalBooks, decimal maxPrice, decimal minPrice) GetBookStatistics(this IEnumerable<Book> list)
        {
            var averagePrice = list.Average(x => x.Price);
            var totalBooks = list.Count();
            var maxPrice = list.Max(x => x.Price);
            var minPrice = list.Min(x => x.Price);

            return (averagePrice, totalBooks, maxPrice, minPrice);
        }

        public static decimal GetAveragePageCount(this IEnumerable<Book> list)
        {
            return (decimal)list.Average(x => x.PageCount);
        }

        public static int GetBookCountByGenre(this IEnumerable<Book> list, string genre)
        {
            return list.Count(x => x.Genre.ToLower() == genre.ToLower());
        }

        public static int GetAvailableBooksCount(this IEnumerable<Book> list)
        {
            return list.Count(x => x.IsAvailable);
        }

        public static int GetBooksCountByAuthor(this IEnumerable<Book> list, string author)
        {
            return list.Count(x => x.Author.ToLower().Contains(author.ToLower()));
        }

    }
}
