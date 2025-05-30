using BookCatalog.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCatalog.DAL.DTO.CalculatedValueModel
{
    public  class GenreCalculatedDTO : GenreDTO
    {
        public decimal Max { get; set; }
        public decimal Min { get; set; }
        public decimal Average { get; set; }

    }
}
