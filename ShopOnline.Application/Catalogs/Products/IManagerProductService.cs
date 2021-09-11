using ShopOnline.ViewModel.Catalogs.Products.DTOs;
using ShopOnline.ViewModel.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.Application.Catalogs.Products
{
    public interface IManagerProductService
    {
        Task<int> Create(ProductViewModel rq);
        Task<bool> Update(ProductUpdateRequest rq);
        Task<bool> UpdatePrice(int productId, decimal newPrice);
        Task<bool> UpdateStock(int productId, int addQuantity);
        Task AddViewCount(int productId); 
        Task<int> Delete(int productId);
        //Task<List<ProductViewModel>> GetAll();
        Task<PagedResult<ProductViewModel>> GetAllPaging(GetProductPagingRequest request);
        
    }
}
