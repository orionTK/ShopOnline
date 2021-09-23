using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopOnline.Application.Catalogs.Products;
using ShopOnline.Application.System.Users;
using ShopOnline.ViewModel.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace ShopOnline.WebBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        public UsersController(IUserService userService, IProductService productService) 
        {
            _userService = userService;
            _productService = productService;
        }

       

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest rq)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }   
            var result = await _userService.Authencate(rq);
            if (string.IsNullOrEmpty(result.ResultObj))
                return BadRequest(result);
            //else
            //    HttpContext.Session.SetString("Token", result);
            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        //FromBody => file json
        public async Task<IActionResult> Register([FromBody] RegisterRequest rq)
        {
            //string pattern = @"(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{6,20})$";
            //Regex rgx = new Regex(pattern);
            //string passWord = rq.Password;
            //if (rgx.IsMatch(passWord))
            //    return BadRequest("Password is absurd");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.Register(rq);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetUserPagingRequest rq)
        {
            var products = await _userService.GetUsersPaging(rq);
            return Ok(products);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UserUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.Update(id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("{id}/roles")]
        public async Task<IActionResult> RoleAssign(Guid id, [FromBody] RoleAssignRequest rq)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.RoleAssign(id, rq);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userService.GetById(id);
            return Ok(user);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _userService.Delete(id);
            return Ok(user);
        }
    }
}
