
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopOnline.Application.Catalogs.Products;
using ShopOnline.ViewModel.Catalog.ProductImages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopOnline.WebBackEnd.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductImagesController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductImagesController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("get-list-images/{productId}")]
        public async Task<IActionResult> GetListImages(int productId)
        {
            var result = await _productService.GetListImages(productId);
            return Ok(result);
        }

        [HttpPost("add-images/{productId}")]
        public async Task<IActionResult> AddImages(int productId, [FromForm] ProductImageCreateRequest request)
        {
            var result = await _productService.AddImages(productId, request);
            if (result == 0)
            {
                return BadRequest($"Don't find productId: {productId}");
            }
            var pm = _productService.GetImageById(result);
            return CreatedAtAction(nameof(GetImageById), new { productId }, pm);
        }

        [HttpDelete("remove-images/{imageId}")]
        public async Task<IActionResult> RemoveImages(int imageId)
        {
            var result = await _productService.RemoveImages(imageId);
            if (result == 0)
            {
                return BadRequest($"Don't find image with id : {imageId}");
            }
            return Ok();
        }

        [HttpPatch("update-images/{imageId}")]
        public async Task<IActionResult> UpdateImages(int imageId, [FromForm] ProductImageUpdateRequest request)
        {
            var result = await _productService.UpdateImages(imageId, request);
            if (result == 0)
            {
                return BadRequest($"Don't find image with id : {imageId}");
            }
            return Ok();
        }

        [HttpGet("get-image-by-id/{imageId}")]
        public async Task<IActionResult> GetImageById([FromQuery] int imageId)
        {
            var result = await _productService.GetImageById(imageId);
            if (result == null)
            {
                return BadRequest($"Don't find image with id : {imageId}");
            }
            return Ok();
        }
    }
}