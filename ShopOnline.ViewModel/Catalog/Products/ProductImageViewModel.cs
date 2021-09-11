using System;
using System.Collections.Generic;
using System.Text;

namespace ShopOnline.ViewModel.Catalog.Products
{
    public class ProductImageViewModel
    {
        public int Id { get; set; }
        public bool IsDefault { get; set; }
        public string FilePath { get; set; }
        public long FileSize { get; set; }

    }
}
