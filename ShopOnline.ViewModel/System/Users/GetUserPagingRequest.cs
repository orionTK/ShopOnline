using ShopOnline.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopOnline.ViewModel.System.Users
{
    public class GetUserPagingRequest : PagingRequestBase
    {
        public string keyword { get; set; }

    }
}
