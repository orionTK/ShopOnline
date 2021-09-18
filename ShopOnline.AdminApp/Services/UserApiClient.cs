using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ShopOnline.ViewModel.Common;
using ShopOnline.ViewModel.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.AdminApp.Services
{
    public class UserApiClient : IUserApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        //for write unit test
        public UserApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }
        public async Task<string> Authenticate(LoginRequest rq)
        {
            var json = JsonConvert.SerializeObject(rq);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var reponse = await client.PostAsync("/api/users/authenticate", httpContent);
            var token = await reponse.Content.ReadAsStringAsync();
            return token;
        }

        public async Task<PagedResult<UserViewModel>> GetUsersPaging(GetUserPagingRequest rq)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", rq.BearerToken); ;
            var response = await client.GetAsync($"/api/users/paging?pageIndex=" +  $"{rq.PageIndex}&pageSize={rq.PageSize}&keyword={rq.keyword}");
            var body = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<PagedResult<UserViewModel>>(body);
            return users;
        }

        public async Task<bool> RegisterUser(RegisterRequest rq)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);

            var json = JsonConvert.SerializeObject(rq);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/users", httpContent);
            
            return response.IsSuccessStatusCode;
        }
    }
}
