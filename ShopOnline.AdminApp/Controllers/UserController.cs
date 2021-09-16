using Microsoft.AspNetCore.Mvc;
using ShopOnline.ViewModel.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopOnline.AdminApp.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login(LoginRequest rq) 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(LoginRequest rq)
        {
            return View();
        }
    }
}
