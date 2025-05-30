using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookCatalog.DAL.Models;

public class Review : EntityBase
{
    public virtual User User { get; set; }
    public Guid UserId { get; set; }
    public virtual Book Book { get; set; }
    public Guid BookId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    [Required(ErrorMessage = "Please fill in the rating")]
    [Display(Name = "Rating")]
    [Range(1, 5, ErrorMessage = "Rating Must be between 1 and  5")]
    public int Rating { get; set; }
}
