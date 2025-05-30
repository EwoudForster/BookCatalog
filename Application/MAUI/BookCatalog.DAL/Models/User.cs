using Microsoft.AspNetCore.Identity;

namespace BookCatalog.DAL.Models;

public class User : IdentityUser<Guid>, IEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public virtual ICollection<Review>? Reviews { get; set; }
}
