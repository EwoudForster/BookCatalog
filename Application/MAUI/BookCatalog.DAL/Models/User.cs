using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BookCatalog.DAL.Models;

public class User : IdentityUser<Guid>, IEntity
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public virtual ICollection<Review>? Reviews { get; set; }
    public virtual ICollection<Order>? Orders { get; set; }
}
