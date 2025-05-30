using BookCatalog.DAL.DTO;

namespace BookCatalog.DAL.DTO.CalculatedValueModel
{
    public class PublisherCalculatedDTO : PublisherDTO
    {
            public decimal Max { get; set; }
            public decimal Min { get; set; }
            public decimal Average { get; set; }

       
    }
}