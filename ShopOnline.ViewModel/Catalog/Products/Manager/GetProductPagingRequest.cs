using ShopOnline.ViewModel.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopOnline.ViewModel.Catalogs.Products.DTOs
{
    public class GetProductPagingRequest :  PagingRequestBase
    {
        public string Keyword { get; set; }
        public List<int> CategoryId { get; set; }
    }
}
