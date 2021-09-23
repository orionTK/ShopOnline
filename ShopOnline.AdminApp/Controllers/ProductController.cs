using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ShopOnline.AdminApp.Services;
using ShopOnline.Utilies.Constants;
using ShopOnline.ViewModel.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopOnline.AdminApp.Controllers
{
    [Authorize]

    public class ProductController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IProductApiClient _productApiClient;
        public ProductController(
            IConfiguration configuration, IProductApiClient productApiClient)
        {
            _configuration = configuration;
            _productApiClient = productApiClient;
        }
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 5)
        {
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);
            //var session = HttpContext.Session.GetString(SystemConstants.AppSettings.Token);
            var rq = new GetManageProductPagingRequest()
            {
                //BearerToken = session,
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                LanguageId = languageId
            };

            var data = await _productApiClient.GetProductsPaging(rq);
            ViewBag.Keyword = keyword;
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(data.ResultObj);
        }

        [HttpGet]
        public IActionResult Create()
        {
           return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {

           if(!ModelState.IsValid)
                return View();

            var result = await _productApiClient.CreateProduct(request);
            if (result)
            {
                TempData["result"] = "Thêm mới sản phẩm dùng thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm sản phẩm thất bại");
            return View(request);
        }
    }
}
