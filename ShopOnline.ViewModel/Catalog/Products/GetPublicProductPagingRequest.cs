using ShopOnline.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopOnline.ViewModel.Catalog.Products 
{
    public class GetPublicProductPagingRequest : PagingRequestBase
    {
        public string languageId { get; set; }
        public int? CategoryId { get; set; }
    }
}
