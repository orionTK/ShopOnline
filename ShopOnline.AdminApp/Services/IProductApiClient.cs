
using ShopOnline.ViewModel.Catalog.Products;
using ShopOnline.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopOnline.AdminApp.Services
{
    public interface IProductApiClient
    {
        Task<ApiResult<PagedResult<ProductViewModel>>> GetProductsPaging(GetManageProductPagingRequest rq);
        Task<bool> CreateProduct(ProductCreateRequest rq);
        Task<ApiResult<bool>> UpdateProduct(ProductUpdateRequest rq);
        Task<ApiResult<bool>> DeleteProduct(int id);
        Task<ApiResult<ProductViewModel>> GetById(int id, string languageId);
        Task<ApiResult<bool>> CategoryAssign(int id, CategoryAssignRequest rq);
        Task<List<ProductViewModel>> GetFeaturedProducts(string languageId, int take);
        Task<List<ProductViewModel>> GetLatestProducts(string languageId, int take);

    }
}
