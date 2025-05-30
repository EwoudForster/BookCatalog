using Microsoft.AspNetCore.Identity;

namespace BookCatalog.DAL.Models;

public class Roles : IdentityRole<Guid>, IEntity
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Description { get; set; }
}
