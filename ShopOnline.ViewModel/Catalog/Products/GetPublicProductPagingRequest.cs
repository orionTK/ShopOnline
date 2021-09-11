using ShopOnline.ViewModel.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopOnline.ViewModel.Catalogs.Products 
{
    public class GetPublicProductPagingRequest : PagingRequestBase
    {
        public int? CategoryId { get; set; }
    }
}
