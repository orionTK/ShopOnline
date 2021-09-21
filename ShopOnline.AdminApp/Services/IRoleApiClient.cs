using Microsoft.AspNetCore.Mvc;
using ShopOnline.ViewModel.Common;
using ShopOnline.ViewModel.System.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopOnline.AdminApp.Services
{
    public interface IRoleApiClient
    {
        Task<ApiResult<List<RoleViewModel>>> GetAll();
        Task<ApiResult<bool>> CreateRole(RoleCreateModel rq);
        Task<ApiResult<bool>> UpdateRole(Guid id, RoleUpdateModel rq);
        Task<ApiResult<bool>> DeleteRole(Guid id);
        Task<ApiResult<RoleViewModel>> GetByIdRole(Guid id);


    }
}
