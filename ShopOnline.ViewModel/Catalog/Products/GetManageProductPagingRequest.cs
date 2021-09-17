using ShopOnline.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopOnline.ViewModel.Catalogs.Products
{
    public class GetManageProductPagingRequest :  PagingRequestBase
    {
        public string Keyword { get; set; }
        public List<int> CategoryId { get; set; }
    }
}
