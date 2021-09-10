using System;
using System.Collections.Generic;
using System.Text;

namespace ShopOnline.Utilies.Exceptions
{
    public class ShopOnlineExeptions : Exception
    {
        public ShopOnlineExeptions()
        {

        }

        public ShopOnlineExeptions(string message) : base(message)
        {

        }

        public ShopOnlineExeptions(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
