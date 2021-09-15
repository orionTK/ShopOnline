using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopOnline.Data.Migrations
{
    public partial class updateSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppConfigs",
                keyColumn: "Key",
                keyValue: "HomeDescription");

            migrationBuilder.DeleteData(
                table: "AppConfigs",
                keyColumn: "Key",
                keyValue: "HomeKeyword");

            migrationBuilder.DeleteData(
                table: "AppConfigs",
                keyColumn: "Key",
                keyValue: "HomeTitle");

            migrationBuilder.DeleteData(
                table: "CategoryTranslations",
                keyColumn: "CategoryTranslationId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CategoryTranslations",
                keyColumn: "CategoryTranslationId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CategoryTranslations",
                keyColumn: "CategoryTranslationId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CategoryTranslations",
                keyColumn: "CategoryTranslationId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProductInCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ProductTranslations",
                keyColumn: "ProductTranslationId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductTranslations",
                keyColumn: "ProductTranslationId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"));

            migrationBuilder.DeleteData(
                table: "Slides",
                keyColumn: "SlideId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Slides",
                keyColumn: "SlideId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Slides",
                keyColumn: "SlideId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Slides",
                keyColumn: "SlideId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Slides",
                keyColumn: "SlideId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Slides",
                keyColumn: "SlideId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"), new Guid("69bd714f-9576-45ba-b5b7-f00649be00de") });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: "en");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: "vi");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AppConfigs",
                columns: new[] { "Key", "Value" },
                values: new object[,]
                {
                    { "HomeTitle", "This is home page of ShopOnlineTK" },
                    { "HomeKeyword", "This is keyword of ShopOnlineTK" },
                    { "HomeDescription", "This is description of ShopOnlineTK" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "IsShowOnHome", "ParentId", "SortOrder", "Status" },
                values: new object[,]
                {
                    { 1, true, null, 1, 1 },
                    { 2, true, null, 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "LanguageId", "IsDefault", "LanguageName" },
                values: new object[,]
                {
                    { "vi", true, "Tiếng Việt" },
                    { "en", false, "English" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "DateCreated", "IsFeatured", "OriginalPrice", "Price", "Stock" },
                values: new object[] { 1, new DateTime(2021, 9, 12, 13, 44, 1, 33, DateTimeKind.Local).AddTicks(4302), null, 100000m, 20000m, 0 });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"), "2e86567e-5fee-4d8e-a313-049edbe01e38", "Administrator role", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "Slides",
                columns: new[] { "SlideId", "Description", "Image", "SlideName", "SortOrder", "Status", "Url" },
                values: new object[,]
                {
                    { 6, "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", "/themes/images/carousel/6.png", "Second Thumbnail label", 6, 1, "#" },
                    { 1, "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", "/themes/images/carousel/1.png", "Second Thumbnail label", 1, 1, "#" },
                    { 2, "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", "/themes/images/carousel/2.png", "Second Thumbnail label", 2, 1, "#" },
                    { 3, "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", "/themes/images/carousel/3.png", "Second Thumbnail label", 3, 1, "#" },
                    { 4, "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", "/themes/images/carousel/4.png", "Second Thumbnail label", 4, 1, "#" },
                    { 5, "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.", "/themes/images/carousel/5.png", "Second Thumbnail label", 5, 1, "#" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"), new Guid("69bd714f-9576-45ba-b5b7-f00649be00de") });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Dob", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"), 0, "69c153f3-ba3d-4cd9-b886-92b0a0a0ab45", new DateTime(2020, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "tedu.international@gmail.com", true, "Toan", "Bach", false, null, "tedu.international@gmail.com", "admin", "AQAAAAEAACcQAAAAEFv0RYaLuvfZb8U88S7/h2UhxCZqlNpkPjl9KmSifyeuIaQO0Vd9rhBJI3daz6HwUw==", null, false, "", false, "admin" });

            migrationBuilder.InsertData(
                table: "CategoryTranslations",
                columns: new[] { "CategoryTranslationId", "CategoryId", "CategoryName", "Details", "LanguageId", "SeoAlias", "SeoDescription", "SeoTitle" },
                values: new object[,]
                {
                    { 1, 1, "Áo nam", null, "vi", "ao-nam", "Sản phẩm áo thời trang nam", "Sản phẩm áo thời trang nam" },
                    { 3, 2, "Áo nữ", null, "vi", "ao-nữ", "Sản phẩm áo thời trang nữ", "Sản phẩm áo thời trang nữ" },
                    { 2, 1, "Men Shirt", null, "en", "men-shirt", "The shirt products for men", "The shirt products for men" },
                    { 4, 2, "Women Shirt", null, "en", "women-shirt", "The shirt products for women", "The shirt products for women" }
                });

            migrationBuilder.InsertData(
                table: "ProductInCategories",
                columns: new[] { "CategoryId", "ProductId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "ProductTranslations",
                columns: new[] { "ProductTranslationId", "Description", "Details", "LanguageId", "ProductId", "ProductName", "SeoAlias", "SeoDescription", "SeoTitle" },
                values: new object[,]
                {
                    { 1, "Áo sơ mi nam trắng Việt Tiến", "Áo sơ mi nam trắng Việt Tiến", "vi", 1, "Áo sơ mi nam trắng Việt Tiến", "ao-nam-so-mi-nam-trang-viet-tien", "Áo sơ mi nam trắng Việt Tiến", "Áo sơ mi nam trắng Việt Tiến" },
                    { 2, "", "Viet Tien Men Shirt", "en", 1, "Viet Tien Men Shirt", "men-shirt", "Viet Tien Men Shirt", "Viet Tien Men Shirt" }
                });
        }
    }
}
