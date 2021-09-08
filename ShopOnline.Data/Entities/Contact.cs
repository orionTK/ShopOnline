using ShopOnline.Data.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopOnline.Data.Entities
{
    public class Contact
    {
        public int ContactId { get; set; }
        public string ContactName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
        //public Status? Status { get; set; }




    }
}
