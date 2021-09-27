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
    [Authorize]
    public class RoleController : Controller
    {
        private readonly IRoleApiClient _roleApiClient;

        public RoleController(IRoleApiClient roleApiClient)
        {
            _roleApiClient = roleApiClient;
        }

     
        public async Task<IActionResult> Index(string keyword)
        {
            var result = await _roleApiClient.GetAllKeyword(keyword);
            ViewBag.Keyword = keyword;
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(result.ResultObj);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleCreateModel rq)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _roleApiClient.CreateRole(rq);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Thêm mới role dùng thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);
            return View(rq);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await _roleApiClient.GetByIdRole(id);
            if (result.IsSuccessed)
            {
                var role = result.ResultObj;
                var updateRequest = new RoleUpdateModel()
                {
                    Description = role.Description,
                    Name = role.Name
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, RoleUpdateModel request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _roleApiClient.UpdateRole(id, request);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Cập nhật quyền dùng thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            return View(new RoleDeleteModel()
            {
                Id = id
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(RoleDeleteModel request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _roleApiClient.DeleteRole(request.Id);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Xóa role thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);
            return View(request);
        }


        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _roleApiClient.GetByIdRole(id);
            return View(result.ResultObj);
        }
    }
}
