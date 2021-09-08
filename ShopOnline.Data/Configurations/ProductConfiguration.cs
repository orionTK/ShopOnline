using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopOnline.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopOnline.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(x => x.ProductId);
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Stock).IsRequired();
            builder.Property(x => x.OriginalPrice).HasDefaultValue(0);
            builder.Property(x => x.ViewCount).HasDefaultValue(0);
            builder.Property(x => x.CreateDated);



        }
    }
}
