namespace BookCatalog.DAL.DTO;

public class UserDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string UserName { get; set; }
    public string NormalizedUserName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public int AccessFailedCount { get; set; }
    public virtual ICollection<ReviewDTOShort>? Reviews { get; set; }
    public virtual ICollection<RoleDTO>? Roles { get; set; }
    public virtual ICollection<OrderDTOShort>? Orders { get; set; }

}
