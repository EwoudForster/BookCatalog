namespace BookCatalog.DAL.Mapping.DTO
{
    public class CreateOrderItemDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid BookId { get; set; }
        public int Amount { get; set; }
    }
}
