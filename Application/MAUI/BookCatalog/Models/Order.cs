namespace BookCatalog.Models
{
    public class Order : EntityBase
    {

        public string Email { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
