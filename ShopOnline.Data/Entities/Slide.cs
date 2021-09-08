using ShopOnline.Data.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopOnline.Data.Entities
{
    public class Slide
    {
        public int SlideId { set; get; }
        public string SlideName { set; get; }
        public string Description { set; get; }
        public string Url { set; get; }

        public string Image { get; set; }
        public int SortOrder { get; set; }
        public Status? Status { set; get; }
    }
}
