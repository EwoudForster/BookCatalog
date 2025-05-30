namespace BookCatalog.DAL.DTO;

public class UserDTOShort
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string UserName { get; set; }
    public string NormalizedUserName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public int AccessFailedCount { get; set; }
}
