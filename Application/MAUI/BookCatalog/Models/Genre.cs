using System.Collections.ObjectModel;

namespace BookCatalog.Models;

public class Genre : EntityBase
{
    public string Name { get; set; }
    public string Description { get; set; }

    public virtual ObservableCollection<Book>? Books { get; set; }
}
