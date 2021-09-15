using Microsoft.AspNetCore.Identity;
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
                    Status=Status.Active
                },
                new Category()
                {
                    CategoryId = 2,
                    IsShowOnHome = true,
                    ParentId = null,
                    SortOrder = 2,
                    Status = Status.Active
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
                   DateCreated = DateTime.Now,
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


            var roleId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC");
            var adminId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE");
            modelBuilder.Entity<Role>().HasData(new Role
            {
                Id = roleId,
                Name = "admin",
                NormalizedName = "admin",
                Description = "Administrator role"
            });

            var hasher = new PasswordHasher<User>();
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = adminId,
                UserName = "meo",
                NormalizedUserName = "meo",
                Email = "meo@gmail.com",
                NormalizedEmail = "meo@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "123456$"),
                SecurityStamp = string.Empty,
                FirstName = "meo",
                LastName = "meo",
                Dob = new DateTime(1998, 02, 03)
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId,
                UserId = adminId
            });

            modelBuilder.Entity<Slide>().HasData(
              new Slide() { SlideId = 1, SlideName = "Second Thumbnail label", Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", SortOrder = 1, Url = "#", Image = "/themes/images/carousel/1.png", Status = Status.Active },
              new Slide() { SlideId = 2, SlideName = "Second Thumbnail label", Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", SortOrder = 2, Url = "#", Image = "/themes/images/carousel/2.png", Status = Status.Active },
              new Slide() { SlideId = 3, SlideName = "Second Thumbnail label", Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", SortOrder = 3, Url = "#", Image = "/themes/images/carousel/3.png", Status = Status.Active },
              new Slide() { SlideId = 4, SlideName = "Second Thumbnail label", Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", SortOrder = 4, Url = "#", Image = "/themes/images/carousel/4.png", Status = Status.Active },
              new Slide() { SlideId = 5, SlideName = "Second Thumbnail label", Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", SortOrder = 5, Url = "#", Image = "/themes/images/carousel/5.png", Status = Status.Active },
              new Slide() { SlideId = 6, SlideName = "Second Thumbnail label", Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", SortOrder = 6, Url = "#", Image = "/themes/images/carousel/6.png", Status = Status.Active }
              );
        }

    }
    
}
