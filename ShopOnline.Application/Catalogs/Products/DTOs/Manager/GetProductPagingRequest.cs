using ShopOnline.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopOnline.Application.Catalogs.Products.DTOs
{
    public class GetProductPagingRequest :  PagingRequestBase
    {
        public string Keyword { get; set; }
        public List<int> CategoryId { get; set; }
    }
}
