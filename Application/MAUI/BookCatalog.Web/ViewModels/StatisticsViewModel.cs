using BookCatalog.DAL.Repositories;

namespace BookCatalog.Web.ViewModels
{
    public class StatisticsViewModel
    {
        public string PageName { get; set; }
        public string PageSentence { get; set; }
        public int TotalBooks { get; set; }
        public decimal Average { get; set; }
        public decimal Max { get; set; }
        public decimal Min { get; set; }
        public string StatisticsType { get; set; }

        public StatisticsViewModel(Dictionary<StatisticsOptions, object> statistics, string pagename, string pagesentence)
        {
            Average = Math.Round((decimal)statistics.FirstOrDefault(p => p.Key == StatisticsOptions.Average).Value, 2);
            Min = (decimal)statistics.FirstOrDefault(p => p.Key == StatisticsOptions.Min).Value;
            Max = (decimal)statistics.FirstOrDefault(p => p.Key == StatisticsOptions.Max).Value;
            TotalBooks = (int)statistics.FirstOrDefault(p => p.Key == StatisticsOptions.TotalBooks).Value;
            StatisticsType = (string)statistics.FirstOrDefault(p => p.Key == StatisticsOptions.StatisticsType).Value;
        }
    }
}
