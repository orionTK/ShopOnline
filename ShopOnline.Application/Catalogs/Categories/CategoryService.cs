using ShopOnline.Application.Common;
using ShopOnline.Data.EF;
using ShopOnline.ViewModel.Catalog.Categories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ShopOnline.ViewModel.Catalog.Products;

namespace ShopOnline.Application.Catalogs.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly ShopOnlineDbContext _context;
        private readonly IStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";

        public CategoryService(ShopOnlineDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }
        public async Task<List<CategoryViewModel>> GetAll(string languageId)
        {
            var query = from c in _context.Categories
                        join ct in _context.CategoryTranslations on c.CategoryId equals ct.CategoryId
                        where ct.LanguageId == languageId
                        select new { c, ct };
            return await query.Select(x => new CategoryViewModel()
            {
                Id = x.c.CategoryId,
                CategoryName = x.ct.CategoryName,
                ParentId = x.c.ParentId
            }).ToListAsync();


        }

        public async Task<CategoryViewModel> GetById(string languageId, int id)
        {
            var query = from c in _context.Categories
                        join ct in _context.CategoryTranslations on c.CategoryId equals ct.CategoryId
                        where ct.LanguageId == languageId && c.CategoryId == id
                        select new { c, ct };
            return await query.Select(x => new CategoryViewModel()
            {
                Id = x.c.CategoryId,
                CategoryName = x.ct.CategoryName,
                ParentId = x.c.ParentId
            }).FirstOrDefaultAsync();
        }
    }
}
