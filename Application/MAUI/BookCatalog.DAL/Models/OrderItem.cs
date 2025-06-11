namespace BookCatalog.DAL.Models
{
    public class OrderItem : EntityBase
    {
        public int Amount { get; set; }
        public Guid BookId { get; set; }
        public virtual Book Book { get; set; }
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}