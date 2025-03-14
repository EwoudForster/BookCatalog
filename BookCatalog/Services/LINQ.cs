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

        public static IEnumerable<Book> Searchw(this IEnumerable<Book> list)
        {
            return list;
        }

        public static IEnumerable<Book> Searchd(this IEnumerable<Book> list)
        {
            return list;
        }

    }
}
