using ShopOnline.ViewModel.Catalog.Categories;
using ShopOnline.ViewModel.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.Application.Catalogs.Categories
{
    public interface ICategoryService
    {
        Task<List<CategoryViewModel>> GetAll(string languageId);
        Task<CategoryViewModel> GetById(string languageId, int id);
    }
}
