
using System.Collections.ObjectModel;

namespace BookCatalog.Models;

    public class BookStore : Location
    {
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public string Name { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string FirstImageUrl{ get; set; }

    public string PhoneNumber { get; set; }
    public ObservableCollection<Picture> Pictures { get; set; }

}
