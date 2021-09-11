using ShopOnline.Application.Catalogs.Products.DTOs;
using ShopOnline.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.Application.Catalogs.Products
{
    public class PublicProductService : IProductService
    {
        public Task<PagedResult<ProductViewModel>> GetAllByCategoryId(GetProductPagingRequest rq)
        {
            throw new NotImplementedException();
        }
    }

}
