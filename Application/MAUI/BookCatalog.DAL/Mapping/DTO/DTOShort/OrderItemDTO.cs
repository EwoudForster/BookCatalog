namespace BookCatalog.DAL.DTO
{
    public class OrderItemDTOShort
    {
        public DateTime CreatedAt { get; set; }
        public Guid Id { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int amount { get; set; }
        public Guid OrderId { get; set; }
    }
}