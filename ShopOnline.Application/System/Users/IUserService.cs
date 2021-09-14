using ShopOnline.ViewModel.Users.System;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.Application.Users.System
{
    public interface IUserService
    {
        Task<bool> Authencate(LoginRequest rq);
        Task<bool> Register(RegisterRequest rq);
    }
}
