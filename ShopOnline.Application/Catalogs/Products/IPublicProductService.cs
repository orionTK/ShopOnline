using ShopOnline.Application.Catalogs.Products.DTOs;
using ShopOnline.Application.Catalogs.Products.DTOs.Public;
using ShopOnline.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.Application.Catalogs.Products
{
    public interface IProductService
    {
        Task<PagedResult<ProductViewModel>> GetAllByCategoryId(PGetProductPagingRequest rq);
    }
}
