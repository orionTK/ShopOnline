using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopOnline.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopOnline.Data
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImages");
            builder.HasKey(x => x.ProductImageId);
            builder.Property(x => x.ProductImageId).UseIdentityColumn();
            builder.Property(x => x.DateCreated);
            builder.Property(x => x.Caption).IsRequired().HasMaxLength(200);
            builder.Property(x => x.ImagePath).IsRequired().HasMaxLength(200);
            builder.Property(x => x.IsDefault).IsRequired();
            builder.Property(x => x.SortOrder).IsRequired();
            builder.Property(x => x.FileSize).IsRequired();
            builder.HasOne(x => x.Product).WithMany(x => x.ProductImages).HasForeignKey(x => x.ProductId);
        }
    }
}
