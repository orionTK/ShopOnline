using ShopOnline.ViewModel.Common;
using ShopOnline.ViewModel.System.Roles;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.Application.System.Roles
{
    public interface IRoleService
    {
        Task<List<RoleViewModel>> GetAll();
        Task<ApiResult<bool>> Create(RoleCreateModel request);
        Task<ApiResult<bool>> Update(Guid id, RoleUpdateModel request);
        Task<ApiResult<bool>> Delete(Guid id);
        Task<ApiResult<RoleViewModel>> GetById(Guid id);
        Task<List<RoleViewModel>> GetAllKeword(string keyword);
    }
}
