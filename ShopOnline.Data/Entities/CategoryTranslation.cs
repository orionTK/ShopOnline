using System;
using System.Collections.Generic;
using System.Text;

namespace ShopOnline.Data.Entities
{
    public class CategoryTranslation
    {
        public int CategoryTranslationId { set; get; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Details { get; set; }
        public string SeoDescription { get; set; }
        public string SeoTitle { get; set; }
        public string SeoAlias { get; set; }
        public string LanguageId { get; set; }
        public Category Category { get; set; }
        public Language Language { get; set; }

    }
}
