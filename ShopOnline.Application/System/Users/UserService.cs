using Microsoft.AspNetCore.Identity;
using ShopOnline.ViewModel.Users.System;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ShopOnline.Data.Entities;
using ShopOnline.Utilies.Exceptions;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace ShopOnline.Application.Users.System
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleInManager;
        private readonly IConfiguration _config;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleInManager = roleInManager;
            _config = configuration;
        }
        //for login
        public async Task<string> Authencate(LoginRequest rq)
        {
            var user = await _userManager.FindByNameAsync(rq.UserName);
            if (user == null) return null;
            //throw new ShopOnlineExeptions($"Can't find username");
            var result = await _signInManager.PasswordSignInAsync(user, rq.Password, rq.RememberMe, true);
            if (!result.Succeeded)
                return null;

            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Role, string.Join(";", roles))
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Isusuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);

            return  new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> Register(RegisterRequest rq)
        {
            var user = new User()
            {
                Dob = rq.DoB,
                Email = rq.Email,
                FirstName = rq.FirstName,
                LastName = rq.LastName,
                PhoneNumber = rq.PhoneNumber,
                UserName = rq.UserName
            };
            var result = await _userManager.CreateAsync(user, rq.Password);
            if (!result.Succeeded) return false;

            return true;
        }
    }
}
