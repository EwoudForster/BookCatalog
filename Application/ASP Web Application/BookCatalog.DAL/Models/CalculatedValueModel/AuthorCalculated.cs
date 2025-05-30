using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCatalog.DAL.Models.CalculatedValueModel
{
    public  class AuthorCalculated : Author
    {
        public decimal Max { get; set; }
        public decimal Min { get; set; }
        public decimal Average { get; set; }

    }
}
