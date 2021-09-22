using ShopOnline.ViewModel.System.Languages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.Application.System.Languages
{
    public interface ILanguageService
    {
        Task<List<LanguageViewModel>> GetAll();
    }
}
