using System;
using System.Collections.Generic;
using System.Text;

namespace ShopOnline.Data.Entities
{
    public class ProductTranslation
    {
        public int ProductTranslationId { set; get; }
        public int ProductId { get; set; }
        public string ProductTranslationtName { get; set; }
        public string Description { get; set; }
        public string Details { get; set; }
        public string SeoDescription { get; set; }
        public string SeoTittle { get; set; }
        public int SeoAlias { get; set; }
        public string LanguageId { get; set; }
        public Product Product { get; set; }
        public Language Language { get; set; }
    }
}
