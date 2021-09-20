using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopOnline.Application.System.Roles;
using ShopOnline.Data.Entities;
using ShopOnline.ViewModel.System.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopOnline.WebBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RolesController(IRoleService role)
        {
            _roleService = role;
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _roleService.GetAll();
            return Ok(roles);
        }

        [HttpGet("{}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var roles = await _roleService.GetById(id);
            return Ok(roles);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] RoleCreateModel rq)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productId = await _roleService.Create(rq);
            if (productId == 0)
            {
                return BadRequest("Can't create a new role"); //400
            }
            var product = await _productService.GetById(productId, request.LanguageId);
            //return Ok(); //200
            return CreatedAtAction(nameof(GetById), new { ProdcutId = productId }, product);

        }
    }
}
