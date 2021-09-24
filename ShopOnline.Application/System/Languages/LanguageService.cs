using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShopOnline.Data.EF;
using ShopOnline.Data.Entities;
using ShopOnline.ViewModel.System.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.Application.System.Languages
{
    public class LanguageService : ILanguageService
    {
        //private readonly UserManager<User> _userManager;
        //private readonly SignInManager<User> _signInManager;
        //private readonly RoleManager<Role> _roleInManager;
        private readonly IConfiguration _config;
        private readonly ShopOnlineDbContext _context;
        public LanguageService(IConfiguration configuration, ShopOnlineDbContext context)
        {
            _config = configuration;
            _context = context;
        }
        public async Task<List<LanguageViewModel>> GetAll()
        {

            var listLanguages = await _context.Languages.Select(
                x => new LanguageViewModel() { 
                LanguageId = x.LanguageId,
                Name = x.LanguageName
                }).ToListAsync();
            return listLanguages;
        }
    }
}
