using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopOnline.Application.Catalogs.Products;
using ShopOnline.ViewModel.Catalog.ProductImages;
using ShopOnline.ViewModel.Catalog.Products;
using ShopOnline.ViewModel.Catalogs.Products;
using ShopOnline.ViewModel.DTO;
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
        private readonly IPublicProductService _publicProductService;
        private readonly IManagerProductService _managerProductService;

        public ProductsController(IPublicProductService publicProductService, IManagerProductService managerProductService)
        {
            _publicProductService = publicProductService;
            _managerProductService = managerProductService;
        }

        [HttpGet("get-all/{languageId}")]
        public async Task<IActionResult> GetAll(string languageId)
        {
            var products = await _publicProductService.GetAll(languageId);
            return Ok(products);

        }

        [HttpGet("get-by-id/{productId}/{languageId}")]
        public async Task<IActionResult> GetById([FromQuery] int productId, string languageId)
        {
            var product =  _publicProductService.GetById(productId, languageId);
            if (product == null)
                return BadRequest("Can't find product");
            return Ok(product.Result);

        }

        [HttpGet("get-all-by-category-id/{languageId}")]
        public async Task<IActionResult> GetAllByCategoryId([FromQuery] string languageId)
        {
            var product = _publicProductService.GetAll( languageId);
            return Ok(product.Result);

        }

        [HttpGet("get-all-category/{languageId}")]
        public async Task<IActionResult> GetAllPaging([FromForm] GetPublicProductPagingRequest rq)
        {
            var product = _publicProductService.GetAll(rq.languageId);

            //var products = _publicProductService.GetAllByCategoryId(languageId, rq);
            return Ok(product.Result);

        }



        [HttpPost("create-product")]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productId = await _managerProductService.Create(request);
            if (productId == 0)
            {
                return BadRequest("Can't create a new product"); //400
            }
            var product = await _publicProductService.GetById(productId, request.LanguageId);
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
            var status = await _managerProductService.Update(request);
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
            var status = await _managerProductService.UpdatePrice(productId, newPrice);
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
            var result = await _managerProductService.Delete(productId);
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
            await _managerProductService.AddViewCount(productId);
            return Ok();

        }


        [HttpPatch("update-stock/{id}/{productId}")]
        public async Task<IActionResult> UpdateStock(int productId, int addQuantity)
        {
            var result = await _managerProductService.UpdateStock(productId, addQuantity);
            if (result == false)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}