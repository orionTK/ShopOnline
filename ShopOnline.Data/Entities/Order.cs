using ShopOnline.Data.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopOnline.Data.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { set; get; }
        public Guid UserId { set; get; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipEmail { get; set; }
        public string ShipPhoneNumber { get; set; }
        public Status? Status { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public User User { get; set; }
    }
}
