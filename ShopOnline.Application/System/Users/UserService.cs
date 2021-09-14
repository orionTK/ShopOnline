using Microsoft.AspNetCore.Identity;
using ShopOnline.ViewModel.Users.System;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ShopOnline.Data.Entities;
using ShopOnline.Utilies.Exceptions;

namespace ShopOnline.Application.Users.System
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<bool> Authencate(LoginRequest rq)
        {
            var user = await _userManager.FindByNameAsync(rq.UserName);
            if (user == null) throw new ShopOnlineExeptions($"Can't find username");
            var result = _signInManager.PasswordSignInAsync(user, rq.Password, rq.RememberMe, );
        }

        public Task<bool> Register(RegisterRequest rq)
        {
            throw new NotImplementedException();
        }
    }
}
