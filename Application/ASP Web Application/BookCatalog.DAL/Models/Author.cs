using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BookCatalog.DAL
{
    public class Author : EntityBase
    {
        // Genre properties
        // Giving the properties a display name and setting the required fields and
        // giving feedback to the user if the input is invalid
        [Required(ErrorMessage = "Please fill in the name of the Genre")]
        [Display(Name = "Title")]
        [StringLength(50, ErrorMessage = "No longer than 50 characters")]
        public string Name { get; set; }

        public virtual List<Book>? Books { get; set; }
    }
}
