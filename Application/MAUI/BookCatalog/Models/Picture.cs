namespace BookCatalog.Models;

public class Picture : EntityBase
{
    public string ImgUrl { get; set; }
    public virtual Book? Book { get; set; }

}
