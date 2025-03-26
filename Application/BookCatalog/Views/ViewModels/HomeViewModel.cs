using BookCatalog.DataLayer;
using BookCatalog.DataLayer.Repositories;
using static System.Net.WebRequestMethods;

namespace BookCatalog.Views.ViewModels
{
    public class HomeViewModel
    {
        public decimal Average { get; set; }
        public IEnumerable<Book> Books { get; set; }
        public decimal Max { get; }
        public decimal Min { get; }
        public int TotalBooks { get; }
        public string StatisticsType { get; }
        public IEnumerable<Book> Carousel { get; set; }

        public int? BookCount { get; set; }

        public HomeViewModel(IEnumerable<Book> books, decimal average, decimal max, decimal min, int totalBooks, BookByStatsOptions statisticsType)
        {
            Books = books;
            Max = max;
            Min = min;
            TotalBooks = totalBooks;
            Average = average;
            Carousel = Books.Take(5);
            switch (statisticsType)
            {
                case BookByStatsOptions.Price:
                    StatisticsType = "€";
                    break;
                case BookByStatsOptions.Page:
                    StatisticsType = "Page(s)";
                    break;
                case BookByStatsOptions.Year:
                    StatisticsType = "Publication year";
                    break;
                default:
                    break;
            }


        }
    }
}
