using ShopOnline.ViewModel.System.Roles;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.Application.System.Roles
{
    public interface IRoleService
    {
        Task<List<RoleModelView>> GetAll();
    }
}
