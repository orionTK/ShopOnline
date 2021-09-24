using Microsoft.AspNetCore.Mvc;
using ShopOnline.Application.Catalogs.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopOnline.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(string languageId)
        {
            var categories = await _categoryService.GetAll(languageId);
            return Ok(categories);
        }
    }
}
