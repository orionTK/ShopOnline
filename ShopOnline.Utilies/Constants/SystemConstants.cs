using System;
using System.Collections.Generic;
using System.Text;

namespace ShopOnline.Utilies.Constants
{
    public class SystemConstants
    {
        public const string MainConnectString = "ShopOnlineDb";
        public const string CartSession = "CartSession";
        public class ProductConstants
        {
            public const string NA = "N/A";
        }

        public class AppSettings
        {
            public const string DefaultLanguageId = "DefaultLanguageId";
            public const string Token = "Token";
            public const string BaseAddress = "BaseAddress";

        }

        public class ProductSettings
        {
            public const int NumberOfFeaturedProducts = 4;
            public const int NumberOfLatestProducts = 6;
        }

    }
}
