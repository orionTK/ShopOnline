using ShopOnline.Data.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopOnline.Data.Entities
{
    public class Promotion
    {
        public int PromotionId { get; set; }
        public string PromotionName { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool ApplyForAll { get; set; }
        public double? DiscountPercent { get; set; }
        public double? DiscountAmount { get; set; }
        public int ProductIds { get; set; }
        public int ProductCategoryIds { get; set; }
        public Status? Status { get; set; }

    }   
}
