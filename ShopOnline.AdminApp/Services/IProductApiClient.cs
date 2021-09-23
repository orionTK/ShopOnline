
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
        Task<ApiResult<bool>> UpdateProduct(Guid id, ProductUpdateRequest rq);
        Task<ApiResult<bool>> DeleteProduct(Guid id);
        Task<ApiResult<ProductViewModel>> GetByIdProduct(Guid id);
    }
}
