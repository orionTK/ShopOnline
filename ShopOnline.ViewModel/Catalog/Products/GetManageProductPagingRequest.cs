using ShopOnline.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopOnline.ViewModel.Catalog.Products
{
    public class GetManageProductPagingRequest :  PagingRequestBase
    {
        public string Keyword { get; set; }
        //public List<int> CategoryIds { get; set; }
        public string LanguageId { get; set; }
        public int? CategoryId { get; set; }
    }
}
