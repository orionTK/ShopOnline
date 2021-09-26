using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ShopOnline.Utilies.Constants;
using ShopOnline.ViewModel.Catalog.Products;
using ShopOnline.ViewModel.Common;
using ShopOnline.ViewModel.System.Users;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.AdminApp.Services
{
    public class ProductApiClient : BaseApiClient, IProductApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        //for write unit test
        public ProductApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        : base(httpClientFactory, configuration, httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ApiResult<PagedResult<ProductViewModel>>> GetProductsPaging(GetManageProductPagingRequest rq)
        {
            var data = await base.GetAsync<PagedResult<ProductViewModel>>($"/api/products/paging?Keyword={rq.Keyword}" + $"&languageId={rq.LanguageId}" + "&pageIndex=" + $"{rq.PageIndex}&pageSize={rq.PageSize}&categoryId={rq.CategoryId}");
            return data;
        }

      

        public async Task<bool> CreateProduct(ProductCreateRequest rq)
        {
            var sessions = _httpContextAccessor
                .HttpContext
                .Session
                .GetString(SystemConstants.AppSettings.Token);

            var languageId = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var requestContent = new MultipartFormDataContent();

            if (rq.ThumbnailImage != null)
            {
                byte[] data;
                using (var br = new BinaryReader(rq.ThumbnailImage.OpenReadStream()))
                {
                    data = br.ReadBytes((int)rq.ThumbnailImage.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "thumbnailImage", rq.ThumbnailImage.FileName);
            }

            requestContent.Add(new StringContent(rq.Price.ToString()), "price");
            requestContent.Add(new StringContent(rq.OriginalPrice.ToString()), "originalPrice");
            requestContent.Add(new StringContent(rq.Stock.ToString()), "stock");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(rq.ProductName) ? "" : rq.ProductName.ToString()), "name");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(rq.Description) ? "" : rq.Description.ToString()), "description");

            requestContent.Add(new StringContent(string.IsNullOrEmpty(rq.Details) ? "" : rq.Details.ToString()), "details");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(rq.SeoDescription) ? "" : rq.SeoDescription.ToString()), "seoDescription");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(rq.SeoTitle) ? "" : rq.SeoTitle.ToString()), "seoTitle");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(rq.SeoAlias) ? "" : rq.SeoAlias.ToString()), "seoAlias");
            requestContent.Add(new StringContent(languageId), "languageId");
            var response = await client.PostAsync($"/api/products/create-product", requestContent);
            return response.IsSuccessStatusCode;
        }

        public Task<ApiResult<bool>> UpdateProduct(int id, ProductUpdateRequest rq)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<bool>> DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        //public async Task<ApiResult<ProductViewModel>> GetByIdProduct(int id)
        //{
        //    var data = await base.GetAsync<PagedResult<ProductViewModel>>($"/api/products/paging?Keyword={rq.Keyword}" + $"&languageId={rq.LanguageId}" + "&pageIndex=" + $"{rq.PageIndex}&pageSize={rq.PageSize}&categoryId={rq.CategoryId}");
        //    return data;
        //}

        public async Task<ApiResult<ProductViewModel>> GetById(int id, string languageId)
        {
            var data = await base.GetAsync<ProductViewModel>($"/api/products/get-by-id/{id}/{languageId}");
            return data;
        }

        public async Task<ApiResult<bool>> CategoryAssign(int id, CategoryAssignRequest rq)
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.AppSettings.Token);
            var client = _httpClientFactory.CreateClient();

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(rq);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/products/{id}/categories", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }

        public async Task<List<ProductViewModel>> GetFeaturedProducts(string languageId, int take)
        {
            var data = await GetListAsync<ProductViewModel>($"/api/products/featured/{languageId}/{take}");
            return data;
        }

        public async Task<List<ProductViewModel>> GetLatestProducts(string languageId, int take)
        {
            var data = await GetListAsync<ProductViewModel>($"/api/products/latest/{languageId}/{take}");
            return data;
        }

    }
}
