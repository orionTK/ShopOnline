using ShopOnline.Application.Catalogs.Products.DTOs;
using ShopOnline.Application.Catalogs.Products.DTOs.Public;
using ShopOnline.Application.DTO;
using ShopOnline.Data.EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace ShopOnline.Application.Catalogs.Products
{
    public class PublicProductService : IProductService
    {
        private readonly ShopOnlineDbContext _context;
        public PublicProductService(ShopOnlineDbContext _context)
        {
            _context = _context;
        }

        public async Task<PagedResult<ProductViewModel>> GetAllByCategoryId(PGetProductPagingRequest rq)
        {
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.ProductId equals pt.ProductId
                        join pic in _context.ProductInCategory on p.ProductId equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.CategoryId
                        select new { p, pt, pic };
            
            if (rq.CategoryId.HasValue && rq.CategoryId > 0)
            {
                query = query.Where(p => p.pic.CategoryId == rq.CategoryId);
            }

            int totalRow = query.Count();
            var data = query.Skip((rq.PageIndex - 1) * rq.PageSize)
                .Take(rq.PageSize)
                .Select(x => new ProductViewModel()
                {
                    ProductId = x.p.ProductId,
                    ProductName = x.pt.ProductName,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    OriginalPrice = x.p.OriginalPrice,
                    DateCreated = x.p.DateCreated,
                    LanguageId = x.pt.LanguageId,
                    Price = x.p.Price,
                    SeoAlias = x.pt.SeoAlias,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount
                }).ToList();

            var pagedResult = new PagedResult<ProductViewModel>()
            {
                TotalRecord = totalRow,
                Items = data
            };
            return pagedResult;
        }

    }

}
