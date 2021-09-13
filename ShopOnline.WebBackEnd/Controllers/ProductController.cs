using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopOnline.Application.Catalogs.Products;
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
    public class ProductController : ControllerBase
    {
        private readonly IPublicProductService _publicProductService;
        private readonly IManagerProductService _managerProductService;

        public ProductController(IPublicProductService publicProductService, IManagerProductService managerProductService)
        {
            _publicProductService = publicProductService;
            _managerProductService = managerProductService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var products = _publicProductService.GetAll();
            return Ok(products);

        }

        [HttpGet("get-all-category/{id}")]
        public async Task<IActionResult> GetAllByCategoryId(GetPublicProductPagingRequest rq)
        {
            var products = _publicProductService.GetAllByCategoryId(rq);
            return Ok(products);

        }

        [HttpGet("get-by-id/{productId}")]
        public async Task<IActionResult> GetById([FromQuery]int productId, string languageId)
        {
            var product = _publicProductService.GetById(productId, languageId);
            if (product == null)
                return BadRequest("Can't find product");
            return Ok(product);

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
                return BadRequest(); //400
            }
            var product = await _publicProductService.GetById(productId, request.LanguageId);
            //return Ok(); //200
            return CreatedAtAction(nameof(GetById), new { ProdcutId = productId}, product);

        }

        [HttpPut("update-product")]
        public async Task<IActionResult> Update([FromBody]ProductUpdateRequest request)
        {
            var status = await _managerProductService.Update(request);
            if (status == false)
            {
                return BadRequest(); //400
            }
            //return Ok(); //200
            return Ok();
        }
        [HttpPut("update-price/{productId}/{newPrice}")]
        public async Task<IActionResult> UpdatePrice([FromQuery]int productId, decimal newPrice)
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

        [HttpPost("add-viewcount/{id}")]
        public async Task<IActionResult> AddViewCount(int productId)
        {
            await _managerProductService.AddViewCount(productId);
            return Ok();

        }

        [HttpGet("get-all-paging")]
        public async Task<IActionResult> GetAllPaging(GetManageProductPagingRequest rq)
        {
            PagedResult<ProductViewModel> pagedResult = await _managerProductService.GetAllPaging(rq);
            return Ok(pagedResult);
        }

        [HttpPut("update-stock/{id}/{productId}")]
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
