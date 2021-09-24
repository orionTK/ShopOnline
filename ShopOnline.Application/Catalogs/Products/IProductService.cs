using Microsoft.AspNetCore.Http;
using ShopOnline.ViewModel.Catalog.ProductImages;
using ShopOnline.ViewModel.Catalog.Products;
using ShopOnline.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.Application.Catalogs.Products
{
    public interface IProductService
    {
        Task<int> Create(ProductCreateRequest rq);
        Task<bool> Update(ProductUpdateRequest rq);
        Task<bool> UpdatePrice(int productId, decimal newPrice);
        Task<bool> UpdateStock(int productId, int addQuantity);
        Task AddViewCount(int productId);
        Task<int> Delete(int productId);
        //Task<List<ProductViewModel>> GetAll();
        Task<PagedResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request);
        Task<int> AddImages(int productId, ProductImageCreateRequest request);
        Task<int> RemoveImages(int imageId);
        Task<int> UpdateImages(int imageId, ProductImageUpdateRequest request);
        Task<List<ProductImageViewModel>> GetListImages(int productId);
        Task<ProductImageViewModel> GetImageById(int imageId);
        Task<PagedResult<ProductViewModel>> GetAllByCategoryId(string laguageId, GetPublicProductPagingRequest rq);
        Task<PagedResult<ProductViewModel>> GetAll(string languageId);
        Task<ProductViewModel> GetById(int productId, string languageId);
        Task<ApiResult<bool>> RoleAssign(int id, CategoryAssignRequest rq);


    }
}