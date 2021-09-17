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
        Task<string> Authencate(LoginRequest rq);
        Task<bool> Register(RegisterRequest rq);
        Task<PagedResult<UserViewModel>> GetUsersPaging(GetUserPagingRequest rq);
    }
}
