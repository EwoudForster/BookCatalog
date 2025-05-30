using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCatalog.DAL.Models
{
    public class MoreInfo : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Book>? Books { get; set; }
    }
}
