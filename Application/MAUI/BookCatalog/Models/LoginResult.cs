using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCatalog.Models
{
    public class LoginResult
    {
        public bool IsSuccess { get; set; }
        public Guid UserId { get; set; }
    }

}
