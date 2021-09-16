using ShopOnline.ViewModel.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ShopOnline.AdminApp.Services
{
    public interface IUserApiClient
    {
       
        Task<string> Authenticate(LoginRequest rq);
        Task<bool> Register(RegisterRequest rq);
    }
}
