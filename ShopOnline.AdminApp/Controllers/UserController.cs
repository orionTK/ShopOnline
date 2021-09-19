using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using ShopOnline.AdminApp.Services;
using ShopOnline.ViewModel.Common;
using ShopOnline.ViewModel.System.Users;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.AdminApp.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserApiClient _userApiClient;
        private readonly IConfiguration _configuration;

        public UserController(IUserApiClient userApiClient,
            IConfiguration configuration)
        {
            _userApiClient = userApiClient;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 2)
        {
            var session = HttpContext.Session.GetString("Token");
            var rq = new GetUserPagingRequest()
            {
                BearerToken = session,
                keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            var data = await _userApiClient.GetUsersPaging(rq);
            return View(data);
        }

        

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            //logout
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "User");
        }

        

        [HttpPost]
        public IActionResult Register(LoginRequest rq)
        {
            return View();
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RegisterRequest rq)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _userApiClient.RegisterUser(rq);
            if (result)
                return RedirectToAction("Index");
            return View(rq);
        }
       
    }
}
