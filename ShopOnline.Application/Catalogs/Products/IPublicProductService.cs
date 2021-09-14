using ShopOnline.Data.Entities;
using ShopOnline.ViewModel.Catalogs.Products;
using ShopOnline.ViewModel.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.Application.Catalogs.Products
{
    public interface IPublicProductService
    {
        Task<PagedResult<ProductViewModel>> GetAllByCategoryId(string languageId, GetPublicProductPagingRequest rq);
        Task<PagedResult<ProductViewModel>> GetAll(string languageId);
        Task<ProductViewModel> GetById(int productId, string languageId);

    }
}
