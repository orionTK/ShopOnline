using ShopOnline.Application.Catalogs.Products.DTOs;
using ShopOnline.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.Application.Catalogs.Products
{
    public interface IManagerProductService
    {
        Task<int> Create(ProductViewModel productCreateRequest);
        Task<int> Update(ProductEditRequest productEditRequest);
        Task<int> Delete(int productId);
        Task<List<ProductViewModel>> GetAll();
        Task<PageViewModel<ProductViewModel>> GetAllPaging(string keyword, int pageIndex, int pageSize);
    }
}
