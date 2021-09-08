using System;
using System.Collections.Generic;
using System.Text;

namespace ShopOnline.Data.Entities
{
    public class Language
    {
        public int LanguageId { get; set; }
        public string LanguageName { get; set; }
        public bool IsDefault { get; set; }
        public List<ProductTranslation> ProductTranslations { get; set; }
        public List<CategoryTranslation> CategoryTranslations { get; set; }
    }
}
