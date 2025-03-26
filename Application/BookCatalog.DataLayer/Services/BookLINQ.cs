using BookCatalog.DataLayer;

namespace BookCatalog.DataLayer.Services
{
    public static class LINQBookService
    {

        // Search for a book by a query using the title, author, publisher or genre
        public static IEnumerable<Book> Search(this IEnumerable<Book> list, string query)
        {
            try
            {

                // Refactor the query to lowercase and then use LINQ to search for the query in the title, author, publisher or genre
                var refactoredQuery = query.ToLower();
                return list.Where(x => x.Title.ToLower().Contains(refactoredQuery) || x.Publisher.ToLower().Contains(refactoredQuery) || x.Author.ToLower().Contains(refactoredQuery) || x.Genre.ToLower().Contains(refactoredQuery));
            }
            catch
            {
                // if something goes wrong, return an empty list
                return new List<Book>();
            }
        }

        // Search for a book by a query using the id
        public static Book Search(this IEnumerable<Book> list, Guid query)
        {
            try
            {

                // Use LINQ to search for a book by its ID
                return list.SingleOrDefault(x => x.Id == query);
            }
            catch
            {
                // exception is needed here because single returns an exception if there are multiple items with the same ID
                return new Book();
            }
        }


        // Order the books by a query
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

        // Group the books by genre
        public static IEnumerable<IGrouping<string, Book>> GroupByGenre(this IEnumerable<Book> list)
        {
            return list.GroupBy(x => x.Genre);
        }

        // Group the books by publication year
        public static IEnumerable<IGrouping<int, Book>> GroupByPublicationYear(this IEnumerable<Book> list)
        {
            return list.GroupBy(x => x.PublicationYear);
        }

        // Get the average price, total books, max price and min price
        public static (decimal averagePrice, int totalBooks, decimal maxPrice, decimal minPrice) GetBookStatistics(this IEnumerable<Book> list)
        {
            var averagePrice = list.Average(x => x.Price);
            var totalBooks = list.Count();
            var maxPrice = list.Max(x => x.Price);
            var minPrice = list.Min(x => x.Price);

            // return the values as a tuple
            return (averagePrice, totalBooks, maxPrice, minPrice);
        }

        // Get the average page count
        public static decimal GetAveragePageCount(this IEnumerable<Book> list)
        {
            return (decimal)list.Average(x => x.PageCount);
        }

        // Get the average publication year
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
