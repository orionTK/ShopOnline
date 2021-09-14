using ShopOnline.Data.EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using ShopOnline.ViewModel.DTO;
using ShopOnline.ViewModel.Catalogs.Products;
using ShopOnline.Data.Entities;
using Microsoft.EntityFrameworkCore;
    
namespace ShopOnline.Application.Catalogs.Products
{
    public class PublicProductService : IPublicProductService
    {
        private readonly ShopOnlineDbContext _context;
        public PublicProductService(ShopOnlineDbContext context)
        {
            _context = context;
        }

        public async Task<ProductViewModel> GetById(int productId, string languageId)
        {
            var product = await _context.Products.FindAsync(productId);
            var productTranslation = await _context.ProductTranslations.FirstOrDefaultAsync(x => x.ProductId == productId && x.LanguageId == languageId);

            var categories = await (from c in _context.Categories
                                    join ct in _context.CatogoryTranslations on c.CategoryId equals ct.CategoryId
                                    join pic in _context.ProductInCategories on c.CategoryId equals pic.CategoryId
                                    where pic.ProductId == productId && ct.LanguageId == languageId
                                    select ct.CategoryName).ToListAsync();

            var image = await _context.ProductImages.Where(x => x.ProductId == productId && x.IsDefault == true).FirstOrDefaultAsync();


            var pv = new ProductViewModel()
            {
                ProductId = product.ProductId,
                DateCreated = product.DateCreated,
                Description = productTranslation != null ? productTranslation.Description : null,
                LanguageId = productTranslation.LanguageId,
                Details = productTranslation != null ? productTranslation.Details : null,
                ProductName = productTranslation != null ? productTranslation.ProductName : null,
                OriginalPrice = product.OriginalPrice,
                Price = product.Price,
                SeoAlias = productTranslation != null ? productTranslation.SeoAlias : null,
                SeoDescription = productTranslation != null ? productTranslation.SeoDescription : null,
                SeoTitle = productTranslation != null ? productTranslation.SeoTitle : null,
                Stock = product.Stock,
                ViewCount = product.ViewCount,
                Categories = categories
                
                //ThumbnailImage = image != null ? image.ImagePath : "no-image.jpg"
            };
            return pv;
        }

        public async Task<PagedResult<ProductViewModel>> GetAll()
        {
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.ProductId equals pt.ProductId
                        //join pic in _context.ProductInCategories on p.ProductId equals pic.ProductId
                        //join c in _context.Categories on pic.CategoryId equals c.CategoryId
                        select new { p, pt};

            int totalRow = query.Count();
            var data = query.Select(x => new ProductViewModel()
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
                    ViewCount = x.p.ViewCount,
                    
                }).ToList();

            var pagedResult = new PagedResult<ProductViewModel>()
            {
                TotalRecord = totalRow,
                Items = data
            };
            return pagedResult;
        }

        public async Task<PagedResult<ProductViewModel>> GetAllByCategoryId(GetPublicProductPagingRequest rq)
        {
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.ProductId equals pt.ProductId
                        join pic in _context.ProductInCategories on p.ProductId equals pic.ProductId
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
