using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopOnline.Application.System.Users;
using ShopOnline.ViewModel.System.Users;
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
        public UsersController(IUserService userService) 
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromForm] LoginRequest rq)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.Authencate(rq);
            if (string.IsNullOrEmpty(result))
                return BadRequest("User or password is incorrect.");
            return Ok(result);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromForm] RegisterRequest rq)
        {
            string pattern = @"(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{6,20})$";
            Regex rgx = new Regex(pattern);
            string passWord = rq.Password;
            if (rgx.IsMatch(passWord))
                return BadRequest("Password is absurd");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.Register(rq);
            if (!result)
                return BadRequest("Register is unsuccessful.");
            return Ok(new { token = result });
        }
    }
}
