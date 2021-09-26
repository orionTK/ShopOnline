using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopOnline.ViewModel.Catalog.Products 
{
    public class ProductCreateRequest
    {
        [Required(ErrorMessage = "Bạn phải nhập giá gốc của sản phẩm")]
        public decimal Price { set; get; }
        [Required(ErrorMessage = "Bạn phải nhập giá bán của sản phẩm")]
        public decimal OriginalPrice { set; get; }
        [Required(ErrorMessage = "Bạn phải số lượng tồn kho")]
        public int Stock { set; get; }

        [Required(ErrorMessage = "Bạn phải nhập tên sản phẩm")]
        public string ProductName { set; get; }
        [Required(ErrorMessage = "Bạn phải nhập mô tả")]
        public string Description { set; get; }
        [Required(ErrorMessage = "Bạn phải nhập chi tiết sản phẩm")]
        public string Details { set; get; }

        public string SeoDescription { set; get; }
        public string SeoTitle { set; get; }

        public string SeoAlias { get; set; }
        public string LanguageId { set; get; }

        public bool? IsFeatured { get; set; }

        public IFormFile ThumbnailImage { get; set; }
    }
}
