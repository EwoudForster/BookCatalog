namespace BookCatalog.Models
{
    public class CartItem
    {
        public Book Book { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => Book.Price * Quantity;
    }

}