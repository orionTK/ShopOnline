using ShopOnline.Application.Catalogs.Products.DTOs;
using ShopOnline.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopOnline.Application.Catalogs.Products
{
    public class PublicProductService : IProductService
    {
        public PageViewModel<ProductViewModel> GetAllByCategoryId(int categoryId, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }
    }

}
