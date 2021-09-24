using ShopOnline.ViewModel.Common;
using ShopOnline.ViewModel.System.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopOnline.AdminApp.Services
{
    public interface ILanguageApiClient
    {

        Task<ApiResult<List<LanguageViewModel>>> GetAll();
    }
}
