using Microsoft.AspNetCore.Mvc;
using ShopOnline.AdminApp.Services;
using ShopOnline.ViewModel.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopOnline.AdminApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserApiClient _userApiClient;
        public UserController(IUserApiClient userApiClient)
        {
            _userApiClient = userApiClient;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Login(LoginRequest rq) 
        {
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }
            var token = _userApiClient.Authenticate(rq);
            return View(token);
        }

        [HttpPost]
        public IActionResult Register(LoginRequest rq)
        {
            return View();
        }
    }
}
