using Microsoft.EntityFrameworkCore;
using ShopOnline.Data.Entities;
using ShopOnline.Data.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopOnline.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppConfig>().HasData(
            new AppConfig() { Key = "HomeTitle", Value = "This is home page of ShopOnlineTK" },
            new AppConfig() { Key = "HomeKeyword", Value = "This is keyword of ShopOnlineTK" },
            new AppConfig() { Key = "HomeDescription", Value = "This is description of ShopOnlineTK" }


            );

            modelBuilder.Entity<Language>().HasData(
               new Language() { LanguageId="vi", LanguageName="Tiếng Việt", IsDefault=true},
               new Language() { LanguageId = "en", LanguageName = "English", IsDefault = false }
               );

            

            modelBuilder.Entity<Category>().HasData(
                new Category() { 
                    CategoryId=1,
                    IsShowOnHome=true, 
                    ParentId=null, 
                    SortOrder=1, 
                    Status=Status.Activate
                },
                new Category()
                {
                    CategoryId = 2,
                    IsShowOnHome = true,
                    ParentId = null,
                    SortOrder = 2,
                    Status = Status.Activate
                });
            modelBuilder.Entity<CategoryTranslation>().HasData(
                      new CategoryTranslation()
                      {
                          CategoryTranslationId = 1,
                          CategoryId = 1,
                          CategoryName = "Áo nam",
                          LanguageId = "vi",
                          SeoAlias = "ao-nam",
                          SeoDescription = "Sản phẩm áo thời trang nam",
                          SeoTitle = "Sản phẩm áo thời trang nam"
                      },
                     new CategoryTranslation()
                     {
                         CategoryTranslationId = 2,
                         CategoryId = 1,
                         CategoryName = "Men Shirt",
                         LanguageId = "en",
                         SeoAlias = "men-shirt",
                         SeoDescription = "The shirt products for men",
                         SeoTitle = "The shirt products for men"
                     },
                     new CategoryTranslation()
                     {
                         CategoryTranslationId = 3,
                         CategoryId = 2,
                         CategoryName = "Áo nữ",
                         LanguageId = "vi",
                         SeoAlias = "ao-nữ",
                         SeoDescription = "Sản phẩm áo thời trang nữ",
                         SeoTitle = "Sản phẩm áo thời trang nữ"
                     },
                     new CategoryTranslation()
                     {
                         CategoryTranslationId = 4,
                         CategoryId = 2,
                         CategoryName = "Women Shirt",
                         LanguageId = "en",
                         SeoAlias = "women-shirt",
                         SeoDescription = "The shirt products for women",
                         SeoTitle = "The shirt products for women"
                     }
                 );

            modelBuilder.Entity<Product>().HasData(
               new Product()
               {
                   ProductId=1,
                   CreateDated = DateTime.Now,
                   OriginalPrice = 100000,
                   Price = 20000,
                   ViewCount = 0,
                  
               });

            modelBuilder.Entity<ProductTranslation>().HasData(
                   new ProductTranslation()
                   {
                       ProductTranslationId = 1,
                       ProductId = 1,
                       ProductName = "Áo sơ mi nam trắng Việt Tiến",
                       LanguageId = "vi",
                       SeoAlias = "ao-nam-so-mi-nam-trang-viet-tien",
                       SeoDescription = "Áo sơ mi nam trắng Việt Tiến",
                       SeoTitle = "Áo sơ mi nam trắng Việt Tiến",
                       Details = "Áo sơ mi nam trắng Việt Tiến",
                       Description = "Áo sơ mi nam trắng Việt Tiến"
                   },
                   new ProductTranslation()
                   {
                       ProductTranslationId = 2,
                       ProductId = 1,
                       ProductName = "Viet Tien Men Shirt",
                       LanguageId = "en",
                       SeoAlias = "men-shirt",
                       SeoDescription = "Viet Tien Men Shirt",
                       SeoTitle = "Viet Tien Men Shirt",
                       Details = "Viet Tien Men Shirt",
                       Description = ""
                   }
               );
            modelBuilder.Entity<ProductInCategory>().HasData(
               new ProductInCategory() { ProductId = 1, CategoryId = 1 }
               );

        }
    }
}
