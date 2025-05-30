using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCatalog.Interfaces
{
        public interface ISelfPopupService
    {
            public Task ShowBottomPopupAsync(BookStore selectedBookStore);
            public Task ShowOrderPopup();
    }
}
