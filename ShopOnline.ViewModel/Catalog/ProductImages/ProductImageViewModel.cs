﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ShopOnline.ViewModel.Catalog.ProductImages
{
    public class ProductImageViewModel
    {
        public int ProductImagesId { get; set; }

        public int ProductId { get; set; }

        public string ImagePath { get; set; }

        public string Caption { get; set; }

        public bool IsDefault { get; set; }

        public DateTime DateCreated { get; set; }

        public int SortOrder { get; set; }

        public long FileSize { get; set; }

    }
}
