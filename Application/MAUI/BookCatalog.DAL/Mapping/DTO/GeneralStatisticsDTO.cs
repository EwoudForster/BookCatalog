using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCatalog.DAL.Models
{
    public class GeneralStatisticsDTO
    {
        public DateTime CreatedAt { get; set; }
        public Guid Id { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int TotalBooks { get; set; }
        public int TotalGenres { get; set; }
        public int TotalAuthors { get; set; }
        public int TotalPublishers { get; set; }
        public double MaxPrice { get; set; }
        public double MinPrice { get; set; }
        public double AveragePrice { get; set; }
    }
}
