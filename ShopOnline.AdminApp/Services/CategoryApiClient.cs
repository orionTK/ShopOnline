using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ShopOnline.ViewModel.Catalog.Categories;
using ShopOnline.ViewModel.Common;
using ShopOnline.ViewModel.System.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ShopOnline.AdminApp.Services
{
    public class CategoryApiClient : BaseApiClient , ICategoryApiClient
    {
        public CategoryApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
            : base(httpClientFactory, configuration, httpContextAccessor)
        {
        }
        public async Task<ApiResult<List<CategoryViewModel>>> GetAll(string languageId)
        {
            return await GetAsync<List<CategoryViewModel>>("/api/languages?languageId=" + $"{languageId}");
        }
    }
}
