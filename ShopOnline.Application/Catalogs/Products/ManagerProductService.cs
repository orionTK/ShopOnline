using Microsoft.AspNetCore.Http;
using ShopOnline.Application.Common;
using ShopOnline.Data.EF;
using ShopOnline.Data.Entities;
using ShopOnline.Utilies.Exceptions;
using ShopOnline.ViewModel.Catalog.Products;
using ShopOnline.ViewModel.Catalogs.Products.DTOs;
using ShopOnline.ViewModel.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
namespace ShopOnline.Application.Catalogs.Products
{
    public class MangerProductService : IManagerProductService
    {
        private readonly ShopOnlineDbContext _context;
        private readonly IStorageService _storageService;

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

            //Save image
            if(rq.ThumbnailImage != null)
            {
                product.ProductImages = new List<ProductImage>()
                {
                    new ProductImage()
                    {
                        Caption = "Image for product",
                        DateCreated = DateTime.Now,
                        FileSize = rq.ThumbnailImage.Length,
                        ImagePath = await this.SaveFile(rq.ThumbnailImage),
                        IsDefault = true,
                        SortOrder = 1

                    }
                };
            }
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

            var thumbnaiImage = _context.ProductImages.Where(x => x.IsDefault == true && x.ProductId == productId);
            foreach(var i in thumbnaiImage)
            {
                await _storageService.DeleteFileAsync(i.ImagePath);
            }
            _context.Products.Remove(p);
            return await _context.SaveChangesAsync();
        }

        public async Task<PagedResult<ProductViewModel>> GetAllPaging(GetProductPagingRequest rq)
        {
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.ProductId equals pt.ProductId
                        join pic in _context.ProductInCategories on p.ProductId equals pic.ProductId
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

            //Save image
            if (rq.ThumbnailImage != null)
            {
                var thumbnaiImage = _context.ProductImages.FirstOrDefault(x => x.IsDefault == true && x.ProductId == rq.ProductId);
                if (thumbnaiImage != null)
                {
                    thumbnaiImage.FileSize = rq.ThumbnailImage.Length;
                    thumbnaiImage.ImagePath = await this.SaveFile(rq.ThumbnailImage);
                    _context.ProductImages.Update(thumbnaiImage);
                }
            }
            
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
       
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }

        public Task<int> AddImages(int imageId, List<IFormFile> files)
        {
            throw new NotImplementedException();

        }

        public Task<int> RemoveImages(int imageId)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateImages(int imageId, string caption, bool isDefault)
        {
            throw new NotImplementedException();
        }

        Task<List<ProductImageViewModel>> IManagerProductService.GetListImages(int productId)
        {
            throw new NotImplementedException();
        }
    }
}

