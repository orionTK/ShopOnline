using System;
using System.Collections.Generic;
using System.Text;

namespace ShopOnline.Data.Entities
{
    public class Cart
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public Guid UserId { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }
        public decimal Price { get; set; }

    }
}
