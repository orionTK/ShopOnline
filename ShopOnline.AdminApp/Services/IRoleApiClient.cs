using Microsoft.AspNetCore.Mvc;
using ShopOnline.ViewModel.Common;
using ShopOnline.ViewModel.System.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopOnline.AdminApp.Services
{
    public interface IRoleApiClient
    {
        Task<ApiResult<List<RoleViewlModel>>> GetAll();
    }
}
