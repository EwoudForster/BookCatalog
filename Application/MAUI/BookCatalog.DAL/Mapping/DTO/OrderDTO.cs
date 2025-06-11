namespace BookCatalog.DAL.DTO
{
    public class OrderDTO
    {
        public DateTime CreatedAt { get; set; }
        public Guid Id { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual ICollection<OrderItemDTO> OrderItems { get; set; }
        public Guid PersonId { get; set; }
        public UserDTO Person { get; set; }
    }
}
