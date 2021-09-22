using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopOnline.ViewModel.System.Users
{
    public class LoginRequest
    {
        [Display(Name="Tài khoản")]
        public string UserName { get; set; }
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }
        [Display(Name = "Nhớ mật khẩu")]
        public bool RememberMe { get; set; }
    }
}
