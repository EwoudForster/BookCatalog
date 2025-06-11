namespace BookCatalog.DAL.DTO
{
    public class OrderItemDTO
    {
        public int amount { get; set; }
        public Guid BookId { get; set; }
        public BookDTO Book { get; set; }
        public Guid OrderId { get; set; }
        public OrderDTOShort Order { get; set; }
    }
}