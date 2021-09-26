﻿using ShopOnline.ViewModel.Catalog.Categories;
using ShopOnline.ViewModel.Catalog.ProductImages;
using ShopOnline.ViewModel.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopOnline.WebApp.Models
{
    public class ProductDetailViewModel
    {
        public CategoryViewModel Category { get; set; }

        public ProductViewModel Product { get; set; }

        public List<ProductViewModel> RelatedProducts { get; set; }

        public List<ProductImageViewModel> ProductImages { get; set; }
    }
}
