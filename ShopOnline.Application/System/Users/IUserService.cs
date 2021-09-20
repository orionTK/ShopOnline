using ShopOnline.ViewModel.System.Users;
using ShopOnline.ViewModel.Common;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ShopOnline.ViewModel.Common;

namespace ShopOnline.Application.System.Users
{
    public interface IUserService
    {
        Task<ApiResult<string>> Authencate(LoginRequest rq);
        Task<ApiResult<bool>> Register(RegisterRequest request);
        Task<ApiResult<PagedResult<UserViewModel>>> GetUsersPaging(GetUserPagingRequest rq);
        Task<ApiResult<bool>> Update(Guid id, UserUpdateRequest request);
        Task<ApiResult<UserViewModel>> GetById(Guid id);
        Task<ApiResult<bool>> Delete(Guid id);
        Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest rq);
    }
}
