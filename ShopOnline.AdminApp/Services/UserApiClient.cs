using ShopOnline.ViewModel.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ShopOnline.AdminApp.Services
{
    public class UserApiClient : IUserApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        //for write unit test
        public UserApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public Task<string> Authenticate(LoginRequest rq)
        {
            var client = _httpClientFactory.CreateClient();
            client.PostAsync
        }

        public Task<bool> Register(RegisterRequest rq)
        {
            throw new NotImplementedException();
        }
    }
}
