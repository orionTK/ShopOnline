using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopOnline.AdminApp.Services;
using ShopOnline.ViewModel.System.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopOnline.AdminApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoleController : Controller
    {
        private readonly IRoleApiClient _roleApiClient;

        public RoleController(IRoleApiClient roleApiClient)
        {
            _roleApiClient = roleApiClient;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _roleApiClient.GetAll();
           
            return View(result.ResultObj);
        }
    }
}
