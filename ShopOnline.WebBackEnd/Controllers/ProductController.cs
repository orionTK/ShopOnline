using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopOnline.Application.Catalogs.Products;
using ShopOnline.ViewModel.Catalogs.Products;
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = _publicProductService.GetAll();
            return Ok(products);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int productId)
        {
            var product = _publicProductService.GetById(productId);
            if (product == null)
                return BadRequest();
            return Ok(product);

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductViewModel request)
        {
            var productId = await _managerProductService.Create(request);
            if (productId == 0)
            {
                return BadRequest(); //400
            }
            var product = await _publicProductService.GetById(productId);
            //return Ok(); //200
            return CreatedAtAction(nameof(GetById), new { ProdcutId = productId}, product);

        }

        public async Task<IActionResult> Update([FromBody] ProductUpdateRequest request)
        {
            var status = await _managerProductService.Update(request);
            if (status == false)
            {
                return BadRequest(); //400
            }
            //return Ok(); //200
            return Ok();
        }
        [HttpPut("price/{id}/{newPrice}")]
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

        [HttpDelete("{id}")]
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
    }
}
