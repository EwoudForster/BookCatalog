namespace BookCatalog.DAL.Models.CalculatedValueModel
{
    public class MoreInfoCalculated : MoreInfo
    {
        public decimal Max { get; set; }
        public decimal Min { get; set; }
        public decimal Average { get; set; }

    }
}
