using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookCatalog.DAL.Models;

public class Picture : EntityBase
{
    public string ImgUrl { get; set; }
    [Display(Name = "Publisher")]
    [ForeignKey("BookId")]
    public virtual Book? Book { get; set; }
    public Guid? BookId { get; set; }

}
