using ShopOnline.Application.Catalogs.Products.DTOs;
using ShopOnline.Application.DTO;
using ShopOnline.Data.EF;
using ShopOnline.Data.Entities;
using ShopOnline.Utilies.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
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


        public async Task<int> Create(ProductViewModel rq)
        {
            var product = new Product()
            {
                Price = rq.Price,
                OriginalPrice = rq.OriginalPrice,
                Stock = rq.Stock,
                ViewCount = 0,
                DateCreated = DateTime.Now,
                ProductTranslations = new List<ProductTranslation>() { 
                    new ProductTranslation()
                    {
                        ProductName = rq.ProductName,
                        Description = rq.Description,
                        Details = rq.Details,
                        SeoDescription = rq.SeoDescription,
                        SeoAlias = rq.SeoAlias,
                        SeoTitle = rq.SeoTitle,
                        LanguageId = rq.LanguageId
                    }
                }
            };
            _context.Products.Add(product);
            return await _context.SaveChangesAsync();
        }

        public async Task AddViewCount(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            product.ViewCount += 1;
            await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int productId)
        {
            var p = await _context.Products.FindAsync(productId);
            if (p == null) throw new ShopOnlineExeptions($"Cannot find a product with id: {productId}");
            _context.Products.Remove(p);
            return await _context.SaveChangesAsync();
        }

        public async Task<PagedResult<ProductViewModel>> GetAllPaging(GetProductPagingRequest rq)
        {
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.ProductId equals pt.ProductId
                        join pic in _context.ProductInCategory on p.ProductId equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.CategoryId
                        select new { p, pt, pic };
            if (!string.IsNullOrEmpty(rq.Keyword))
            {
                query = query.Where(x => x.pt.ProductName.Contains(rq.Keyword));
            }

            if (rq.CategoryId.Count > 0)
            {
                query = query.Where(p => rq.CategoryId.Contains(p.pic.CategoryId));
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

        public async Task<bool> Update(ProductUpdateRequest rq)
        {
            var p = await _context.Products.FindAsync(rq.ProductId);
            var pt =  _context.ProductTranslations.FirstOrDefault(x => x.ProductId == rq.ProductId);
            if (p == null || pt ==null) throw new ShopOnlineExeptions($"Don't find a product with id: {rq.ProductId}");
            p.Price = rq.Price;
            pt.ProductName = rq.ProductName;
            pt.Description = rq.Description;
            pt.Details = rq.Details;
            pt.SeoDescription = rq.SeoDescription;
            pt.SeoAlias = rq.SeoAlias;
            pt.SeoTitle = rq.SeoTitle;
            pt.LanguageId = rq.LanguageId;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdatePrice(int productId, decimal newPrice)
        {
            var p = await _context.Products.FindAsync(productId);
            if (p == null) throw new ShopOnlineExeptions($"Don't find a product with id: {productId}");
            p.Price = newPrice;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateStock(int productId, int addQuantity)
        {
            var p = await _context.Products.FindAsync(productId);
            if (p == null) throw new ShopOnlineExeptions($"Don't find a product with id: {productId}");
            p.Stock += addQuantity;

            return await _context.SaveChangesAsync() > 0;
        }
       
    }
}

