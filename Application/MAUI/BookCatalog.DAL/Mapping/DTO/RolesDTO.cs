namespace BookCatalog.DAL.DTO;

public class RoleDTO
{
    public string Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Name { get; set; }
    public virtual ICollection<UserDTOShort> Users { get; set; }

}
