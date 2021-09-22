﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using ShopOnline.AdminApp.Services;
using ShopOnline.Utilies.Constants;
using ShopOnline.ViewModel.System.Users;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.AdminApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserApiClient _userApiClient;
        private readonly IConfiguration _configuration;

        public LoginController(IUserApiClient userApiClient,
            IConfiguration configuration)
        {
            _userApiClient = userApiClient;
            _configuration = configuration;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }

        

        [HttpPost]
        public async Task<IActionResult> Index(LoginRequest rq)
        {

            if (!ModelState.IsValid)
                return View();

            var result = await _userApiClient.Authenticate(rq);
            if (result.ResultObj == null)
            {
                ModelState.AddModelError("", result.Message);
                return View();
            }
            var userPrincical = this.ValidationToken(result.ResultObj);
            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                //luu 
                IsPersistent = false
            };
            //set language
            HttpContext.Session.SetString(SystemConstants.AppSettings.DefaultLanguageId, _configuration[SystemConstants.AppSettings.DefaultLanguageId]);
            //set token
            HttpContext.Session.SetString(SystemConstants.AppSettings.Token, result.ResultObj);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                userPrincical,
                authProperties);
            return RedirectToAction("Index", "Home");
        }

     
        private ClaimsPrincipal ValidationToken(string jwtToken)
        {
            IdentityModelEventSource.ShowPII = true;

            SecurityToken validatedToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters();

            IdentityModelEventSource.ShowPII = true;

            validationParameters.ValidAudience = _configuration["Tokens:Issuer"];
            validationParameters.ValidIssuer = _configuration["Tokens:Issuer"];
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));

            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);

            return principal;
        }
    }
}
