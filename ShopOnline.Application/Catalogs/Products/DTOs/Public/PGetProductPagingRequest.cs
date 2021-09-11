using ShopOnline.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopOnline.Application.Catalogs.Products.DTOs.Public
{
    public class PGetProductPagingRequest : PagingRequestBase
    {
        public int? CategoryId { get; set; }
    }
}
