using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopOnline.AdminApp.Models;
using ShopOnline.AdminApp.Services;
using ShopOnline.Utilies.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopOnline.AdminApp.Controllers.Components
{
    public class NavigationViewComponent : ViewComponent
    {
        private readonly ILanguageApiClient _languageApiClient;
        public NavigationViewComponent(ILanguageApiClient languageApiClient)
        {
            _languageApiClient = languageApiClient;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var languages = await _languageApiClient.GetAll();
            var currentLanguageId = HttpContext
                .Session
                .GetString(SystemConstants.AppSettings.DefaultLanguageId);
            var items = languages.ResultObj.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.LanguageId.ToString(),
                Selected = currentLanguageId == null ? x.IsDefault : currentLanguageId == x.LanguageId.ToString()
            });

            var navigationVm = new NavigationViewModel()
            {
                CurrentLanguageId = currentLanguageId,
                Languages = items.ToList()
            };


            return await Task.FromResult((IViewComponentResult) View("Default", navigationVm));
        }
    }
}
