using ShopOnline.Application.Catalogs.Products.DTOs;
using ShopOnline.Application.DTO;
using ShopOnline.Data.EF;
using ShopOnline.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.Application.Catalogs.Products
{
    public class MangerProductService : IManagerProductService
    {
        private readonly ShopOnlineDbContext _context;
        public MangerProductService(ShopOnlineDbContext _context)
        {
            _context = _context;
        }
        public async Task<int> Create(ProductViewModel productCreateRequest)
        {
            var product = new Product()
            {
                Price = productCreateRequest.Price
            };
            _context.Products.Add(product);
            return await _context.SaveChangesAsync();
        }

        public Task<int> Delete(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<PagedResult<ProductViewModel>> GetAllPaging(GetProductPagingRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(ProductUpdateRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
}
