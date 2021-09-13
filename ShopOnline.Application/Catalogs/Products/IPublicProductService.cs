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
        Task<PagedResult<ProductViewModel>> GetAllByCategoryId(GetPublicProductPagingRequest rq);
        Task<List<ProductViewModel>> GetAll();
        Task<ProductViewModel> GetById(int productId, string languageId);

    }
}
