using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopOnline.Data.Entities;
using ShopOnline.ViewModel.Common;
using ShopOnline.ViewModel.System.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.Application.System.Roles
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<Role> _roleManager;

        public RoleService(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<ApiResult<bool>> Delete(Guid id)
        {
            //var role = await _roleManager.Roles
            //    .Where(x => x.Id == id).FirstOrDefaultAsync();
            var role = await _roleManager.FindByIdAsync(id.ToString());

            if (role == null)
            {
                return new ApiErrorResult<bool>("Role không tồn tại");
            }
            
            var reult = await _roleManager.DeleteAsync(role);
            if (reult.Succeeded)
                return new ApiSuccessResult<bool>();

            return new ApiErrorResult<bool>("Xóa không thành công");
        }

        public async Task<List<RoleViewModel>> GetAll()
        {
            var roles = await _roleManager.Roles
                .Select(x => new RoleViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                }).ToListAsync();

            return roles;
        }

        public async Task<ApiResult<RoleViewModel>> GetById(Guid id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());


            if (role == null)
            {
                return new ApiErrorResult<RoleViewModel>("Role không tồn tại");
            }
            var roleVM = new RoleViewModel()
            {
                Description = role.Description,
                Name = role.Name,
                Id = role.Id
            };
            return new ApiSuccessResult<RoleViewModel>(roleVM);
        }

        public async Task<ApiResult<bool>> Create(RoleCreateModel request)
        {
            var role = await _roleManager.FindByNameAsync(request.Name);
            if (role != null)
            {
                return new ApiErrorResult<bool>("Role đã tồn tại");
            }

            role = new Role()
            {
                Name = request.Name,
                Description = request.Description
            };
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }

            return new ApiErrorResult<bool>("Tạo role không thành công");
        }

        public async Task<ApiResult<bool>> Update(Guid id, RoleUpdateModel request)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role == null)
            {
                return new ApiErrorResult<bool>("Role không tồn tại");
            }
            role.Name = request.Name;
            role.Description = request.Description;
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Cập nhật không thành công");
        }
    }
}
