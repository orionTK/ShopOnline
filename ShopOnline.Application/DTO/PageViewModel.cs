using System;
using System.Collections.Generic;
using System.Text;

namespace ShopOnline.Application.DTO
{
    public class PageViewModel<T>
    {
        public List<T> Items { get; set; }
        public int TotalRecord { get; set; }
    }
}
