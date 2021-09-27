using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ShopOnline.Application.Common;
using ShopOnline.Data.EF;
using ShopOnline.Data.Entities;
using ShopOnline.Utilies.Constants;
using ShopOnline.Utilies.Exceptions;
using ShopOnline.ViewModel.Catalog.ProductImages;
using ShopOnline.ViewModel.Catalog.Products;
using ShopOnline.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
namespace ShopOnline.Application.Catalogs.Products
{
    public class ProductService : IProductService
    {
        private readonly ShopOnlineDbContext _context;
        private readonly IStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";

        public ProductService(ShopOnlineDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }


        public async Task<int> Create(ProductCreateRequest rq)
        {
            var languages = _context.Languages;
            var translations = new List<ProductTranslation>();
            foreach (var language in languages)
            {
                if (language.LanguageId == rq.LanguageId)
                {
                    translations.Add(new ProductTranslation()
                    {
                        ProductName = rq.ProductName,
                        Description = rq.Description,
                        Details = rq.Details,
                        SeoDescription = rq.SeoDescription,
                        SeoAlias = rq.SeoAlias,
                        SeoTitle = rq.SeoTitle,
                        LanguageId = rq.LanguageId
                    });
                }
                else
                {
                    translations.Add(new ProductTranslation()
                    {
                        ProductName = SystemConstants.ProductConstants.NA,
                        Description = SystemConstants.ProductConstants.NA,
                        SeoAlias = SystemConstants.ProductConstants.NA,
                        SeoDescription = SystemConstants.ProductConstants.NA,
                        Details = SystemConstants.ProductConstants.NA,
                        SeoTitle = SystemConstants.ProductConstants.NA,
                        LanguageId = language.LanguageId
                    });
                }
            }
            var product = new Product()
            {
                Price = rq.Price,
                OriginalPrice = rq.OriginalPrice,
                Stock = rq.Stock,
                ViewCount = 0,
                DateCreated = DateTime.Now,
                ProductTranslations = translations
            };
            //Save image
            if (rq.ThumbnailImage != null)
            {
                product.ProductImages = new List<ProductImage>()
                {
                    new ProductImage()
                    {
                        Caption = "Thumbnail image",
                        DateCreated = DateTime.Now,
                        FileSize = rq.ThumbnailImage.Length,
                        ImagePath = await this.SaveFile(rq.ThumbnailImage),
                        IsDefault = true,
                        SortOrder = 1
                    }
                };
            }
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product.ProductId;
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
            foreach (var i in thumbnaiImage)
            {
                await _storageService.DeleteFileAsync(i.ImagePath);
            }
            _context.Products.Remove(p);
            return await _context.SaveChangesAsync();
        }

        public async Task<PagedResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest rq)
        {
            //user left join
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.ProductId equals pt.ProductId
                        join pic in _context.ProductInCategories on p.ProductId equals pic.ProductId into ppic
                        from pic in ppic.DefaultIfEmpty()
                        join c in _context.Categories on pic.CategoryId equals c.CategoryId into picc
                        from c in picc.DefaultIfEmpty()
                        join pi in _context.ProductImages on p.ProductId equals pi.ProductId into ppi
                        from pi in ppi.DefaultIfEmpty()
                       where pt.LanguageId == rq.LanguageId 
                       
                        select new { p, pt, pic, pi }
     
                        ;
            if (!string.IsNullOrEmpty(rq.Keyword))
            {
                query = query.Where(x => x.pt.ProductName.Contains(rq.Keyword));
            }

            if (rq.CategoryId != null && rq.CategoryId > 0)
            {
                query = query
                    .Where(p => p.pic.CategoryId == rq.CategoryId)
                    ;
            }

            int totalRow = query.Count();
            var data = await query.Skip((rq.PageIndex - 1) * rq.PageSize)
                .Take(rq.PageSize)
                .Select(x => new ProductViewModel()
                {
                    ProductId = x.p.ProductId,
                    ProductName = x.pt.ProductName == null ? SystemConstants.ProductConstants.NA :x.pt.ProductName,
                    Description = x.pt.Description == null ? SystemConstants.ProductConstants.NA : x.pt.Description,
                    Details = x.pt.Details == null ? SystemConstants.ProductConstants.NA : x.pt.Details,
                    OriginalPrice = x.p.OriginalPrice,
                    DateCreated = x.p.DateCreated,
                    LanguageId = x.pt.LanguageId,
                    Price = x.p.Price,
                    SeoAlias = x.pt.SeoAlias == null ? SystemConstants.ProductConstants.NA : x.pt.SeoAlias,
                    SeoDescription = x.pt.SeoDescription == null ? SystemConstants.ProductConstants.NA : x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle == null ? SystemConstants.ProductConstants.NA : x.pt.SeoTitle,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount,
                    //Categories = x.
                }).ToListAsync();

            var pagedResult = new PagedResult<ProductViewModel>()
            {
                TotalRecords = totalRow,
                PageIndex = rq.PageIndex,
                PageSize = rq.PageSize,
                Items = data
            };
            return pagedResult;
        }

        public async Task<List<ProductViewModel>> GetFeaturedProducts(string languageId, int take)
        {
            //1. Select join
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.ProductId equals pt.ProductId
                        join pic in _context.ProductInCategories on p.ProductId equals pic.ProductId into ppic
                        from pic in ppic.DefaultIfEmpty()
                        join pi in _context.ProductImages on p.ProductId equals pi.ProductId into ppi
                        from pi in ppi.DefaultIfEmpty()
                        join c in _context.Categories on pic.CategoryId equals c.CategoryId into picc
                        from c in picc.DefaultIfEmpty()
                        where pt.LanguageId == languageId && (pi == null || pi.IsDefault == true)
                        && p.IsFeatured == true
                        select new { p, pt, pic, pi };

            var data = await query.OrderByDescending(x => x.p.DateCreated).Take(take)
                .Select(x => new ProductViewModel()
                {
                    ProductId = x.p.ProductId,
                    ProductName = x.pt.ProductName,
                    DateCreated = x.p.DateCreated,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    LanguageId = x.pt.LanguageId,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    SeoAlias = x.pt.SeoAlias,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount,
                    ThumbnailImage = x.pi.ImagePath
                }).ToListAsync();

            return data;
        }

        public async Task<bool> Update(ProductUpdateRequest rq)
        {
            var p = await _context.Products.FindAsync(rq.ProductId);
            var pt = _context.ProductTranslations.FirstOrDefault(x => x.ProductId == rq.ProductId);
            if (p == null || pt == null) throw new ShopOnlineExeptions($"Don't find a product with id: {rq.ProductId}");

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

            p.Price = rq.Price != null ? rq.Price : p.Price;
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

        public async Task<int> AddImages(int productId, ProductImageCreateRequest request)
        {
            if (await _context.ProductImages.FindAsync(productId) == null)
                throw new ShopOnlineExeptions($"Don't find productId: {productId}");
            var productImage = new ProductImage()
            {
                Caption = request.Caption,
                DateCreated = DateTime.Now,
                IsDefault = request.IsDefault,
                ProductId = productId,
                SortOrder = request.SortOrder
            };

            if (request.ImageFile != null)
            {
                productImage.ImagePath = await this.SaveFile(request.ImageFile);
                productImage.FileSize = request.ImageFile.Length;
            }
            _context.ProductImages.Add(productImage);
            await _context.SaveChangesAsync();
            return productImage.ProductImageId;
        }

        public async Task<int> RemoveImages(int imageId)
        {
            var productImage = await _context.ProductImages.FindAsync(imageId);
            if (productImage == null)
            {
                throw new ShopOnlineExeptions($"Can't fint an image with id: {imageId}");

            }
            _context.ProductImages.Remove(productImage);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateImages(int imageId, ProductImageUpdateRequest request)
        {
            var productImage = await _context.ProductImages.FindAsync(imageId);
            if (productImage == null)
                throw new ShopOnlineExeptions($"Can't fint an image with id: {imageId}");
            productImage.Caption = request.Caption;
            productImage.IsDefault = request.IsDefault;
            productImage.SortOrder = request.SortOrder;
            if (request.ImageFile != null)
            {
                productImage.ImagePath = await this.SaveFile(request.ImageFile);
                productImage.FileSize = request.ImageFile.Length;
            }
            _context.ProductImages.Update(productImage);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<ProductImageViewModel>> GetListImages(int productId)
        {
            return await _context.ProductImages.Where(x => x.ProductId == productId)
                .Select(x => new ProductImageViewModel()
                {
                    Caption = x.Caption,
                    DateCreated = x.DateCreated,
                    FileSize = x.FileSize,
                    ProductImagesId = x.ProductImageId,
                    ImagePath = x.ImagePath,
                    IsDefault = x.IsDefault,
                    ProductId = x.ProductId,
                    SortOrder = x.SortOrder
                }).ToListAsync();
        }
        public async Task<ProductImageViewModel> GetImageById(int imageId)
        {
            var pm = await _context.ProductImages.FindAsync(imageId);
            if (pm == null) throw new ShopOnlineExeptions($"Don't find image with image id: {imageId}");
            return new ProductImageViewModel()
            {
                Caption = pm.Caption,
                DateCreated = pm.DateCreated,
                FileSize = pm.FileSize,
                ImagePath = pm.ImagePath,
                IsDefault = pm.IsDefault,
                ProductId = pm.ProductId,
                ProductImagesId = pm.ProductId,
                SortOrder = pm.SortOrder
            };
        }

        public async Task<ProductViewModel> GetById(int id, string languageId)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) throw new ShopOnlineExeptions($"Don't find product with image id: {id}");

            var productTranslation = await _context.ProductTranslations.FirstOrDefaultAsync(x => x.ProductId == id && x.LanguageId == languageId);

            var categories = await (from c in _context.Categories
                                    join ct in _context.CategoryTranslations on c.CategoryId equals ct.CategoryId
                                    join pic in _context.ProductInCategories on c.CategoryId equals pic.CategoryId
                                    where pic.ProductId == id && ct.LanguageId == languageId
                                    select ct.CategoryName).ToListAsync();

            var image = await _context.ProductImages.Where(x => x.ProductId == id && x.IsDefault == true).FirstOrDefaultAsync();

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
                Categories = categories,

                ThumbnailImage = image != null ? image.ImagePath : "no-image.jpg"
            };
            return pv;
        }

        public async Task<PagedResult<ProductViewModel>> GetAll(string languageId)
        {
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.ProductId equals pt.ProductId
                        join pic in _context.ProductInCategories on p.ProductId equals pic.ProductId
                        //join c in _context.Categories on pic.CategoryIds equals c.CategoryIds
                        where pt.LanguageId == languageId
                        select new { p, pt };

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
                TotalRecords = totalRow,
                Items = data
            };
            return pagedResult;
        }


        public async Task<PagedResult<ProductViewModel>> GetAllByCategoryId(string languageId, GetPublicProductPagingRequest rq)
        {
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.ProductId equals pt.ProductId
                        join pic in _context.ProductInCategories on p.ProductId equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.CategoryId
                        where pt.LanguageId == languageId
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
                TotalRecords = totalRow,
                PageIndex = rq.PageIndex,
                PageSize = rq.PageSize,
                Items = data
            };
            return pagedResult;
        }

        public async Task<ApiResult<bool>> CategoryAssign(int id, CategoryAssignRequest rq)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return new ApiErrorResult<bool>($"Sản phẩm với id {id} không tồn tại");
            }
            foreach (var category in rq.Categories)
            {
                var productInCategory = await _context.ProductInCategories
                    .FirstOrDefaultAsync(x => x.CategoryId == int.Parse(category.Id)
                    && x.ProductId == id);
                if (productInCategory != null && category.Selected == false)
                {
                    _context.ProductInCategories.Remove(productInCategory);
                }
                else if (productInCategory == null && category.Selected)
                {
                    await _context.ProductInCategories.AddAsync(new ProductInCategory()
                    {
                        CategoryId = int.Parse(category.Id),
                        ProductId = id
                    });
                }
            }
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();

        }

    }
}

