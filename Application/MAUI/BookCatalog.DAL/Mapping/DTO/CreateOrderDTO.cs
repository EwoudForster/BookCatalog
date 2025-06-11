namespace BookCatalog.DAL.Mapping.DTO
{
    public class CreateOrderDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Email { get; set; }
        public List<CreateOrderItemDTO> OrderItems { get; set; }
    }

}
