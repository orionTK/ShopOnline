using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopOnline.Application.Catalogs.Products;
using ShopOnline.ViewModel.Catalog.ProductImages;
using ShopOnline.ViewModel.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopOnline.WebBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("get-all/{languageId}")]
        public async Task<IActionResult> GetAll(string languageId)
        {
            var products = await _productService.GetAll(languageId);
            return Ok(products);

        }

        [HttpGet("get-by-id/{id}/{languageId}")]
        public async Task<IActionResult> GetById(int id, string languageId)
        {
            var product =  _productService.GetById(id, languageId);
            if (product == null)
                return BadRequest("Can't find product");
            return Ok(product.Result);

        }

        [HttpGet("get-all-by-category-id/{languageId}")]
        public async Task<IActionResult> GetAllByCategoryId([FromQuery] string languageId)
        {
            var product = _productService.GetAll( languageId);
            return Ok(product.Result);

        }

        [HttpGet("get-all-category/{languageId}")]
        public async Task<IActionResult> GetAll([FromQuery] GetPublicProductPagingRequest rq)
        {
            var product = _productService.GetAll(rq.languageId);

            //var products = _publicProductService.GetAllByCategoryId(languageId, rq);
            return Ok(product.Result);

        }

        

        [HttpPost("create-product")]
        [Consumes("multipart/form-data")]
        //chap nhan doi tuong form
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productId = await _productService.Create(request);
            if (productId == 0)
            {
                return BadRequest("Không tạo được sản phẩm"); //400
            }
            var product = await _productService.GetById(productId, request.LanguageId);
            //return Ok(); //200
            return CreatedAtAction(nameof(GetById), new { ProdcutId = productId }, product);

        }

        [HttpPut("update-product")]
        public async Task<IActionResult> Update([FromForm] ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var status = await _productService.Update(request);
            if (status == false)
            {
                return BadRequest(); //400
            }
            //return Ok(); //200
            return Ok();
        }
        [HttpPut("update-price/{productId}/{newPrice}")]
        public async Task<IActionResult> UpdatePrice([FromQuery] int productId, decimal newPrice)
        {
            var status = await _productService.UpdatePrice(productId, newPrice);
            if (status == false)
            {
                return BadRequest(); //400
            }
            //return Ok(); //200
            return Ok();
        }

        [HttpDelete("delete-product/{productId}")]
        public async Task<IActionResult> Delete(int productId)
        {
            var result = await _productService.Delete(productId);
            if (result == 0)
            {
                return BadRequest(); //400
            }
            //return Ok(); //200
            return Ok();
        }

        [HttpPost("add-viewcount/{productId}")] 
        public async Task<IActionResult> AddViewCount(int productId)
        {
            await _productService.AddViewCount(productId);
            return Ok();

        }


        [HttpPatch("update-stock/{id}/{productId}")]
        public async Task<IActionResult> UpdateStock(int productId, int addQuantity)
        {
            var result = await _productService.UpdateStock(productId, addQuantity);
            if (result == false)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetManageProductPagingRequest rq)
        {
            var product = await _productService.GetAllPaging(rq);

            //var products = _publicProductService.GetAllByCategoryId(languageId, rq);
            return Ok(product);

        }

        [HttpPut("{id}/categories")]
        [Authorize]
        public async Task<IActionResult> CategoryAssign(int id, [FromBody] CategoryAssignRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _productService.CategoryAssign(id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}