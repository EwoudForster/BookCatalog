namespace BookCatalog.Models
{
    public class OrderItem : EntityBase
    {
        public Guid BookId { get; set; }
        public int Amount { get; set; }
    }
}