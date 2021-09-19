using System;
using System.Collections.Generic;
using System.Text;

namespace ShopOnline.ViewModel.Common
{
    public class PagedResultBase
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        // is used for paging
        public int PageCount
        {
            get
            {
                var pageCount = (double)TotalRecords / PageSize;
                return (int)Math.Ceiling(pageCount);
            }
        }
    }
}
