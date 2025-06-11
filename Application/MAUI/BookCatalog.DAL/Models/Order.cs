namespace BookCatalog.DAL.Models
{
    public class Order : EntityBase
    {

        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public Guid PersonId { get; set; }
        public virtual User Person { get; set; }
    }
}
