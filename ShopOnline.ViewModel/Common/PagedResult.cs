using System;
using System.Collections.Generic;
using System.Text;

namespace ShopOnline.ViewModel.Common
{
    public class PagedResult<T> : PagedResultBase
    {
        public List<T> Items { get; set; }
    }
}
