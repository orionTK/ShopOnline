using Microsoft.EntityFrameworkCore;
using ShopOnline.Data.Configurations;
using ShopOnline.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopOnline.Data.EF
{
    public class ShopOnlineDbContext : DbContext
    {
        public ShopOnlineDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new AppConfigConfiguration());
            modelBuilder.ApplyConfiguration(new CartConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());

        }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryTranslation> CatogoryTranslations { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Product> Products { get; set; }
        
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<ProductTranslation> ProductTranslations { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<AppConfig> AppConfigs { get; set; }

    }
}
