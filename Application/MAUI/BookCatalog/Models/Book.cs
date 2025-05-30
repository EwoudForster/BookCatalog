using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookCatalog.Models;

public class Book : EntityBase
{

    public string Title { get; set; }
    public virtual ObservableCollection<Author>? Authors { get; set; }
    public int PublicationYear { get; set; }
    public virtual ObservableCollection<Genre>? Genres { get; set; }
    public string ISBN { get; set; }
    public virtual Publisher? Publisher { get; set; }
    public int PageCount { get; set; }
    public decimal Price { get; set; }

    public bool IsAvailable { get; set; }
    public double AverageRating { get; set; }
    public string FirstImageUrl{ get; set; }

    public string? Description { get; set; }
    public virtual ObservableCollection<Picture>? Pictures { get; set; }
    public virtual ObservableCollection<MoreInfo>? MoreInfos { get; set; }
    public virtual ObservableCollection<Review>? Reviews { get; set; }
}