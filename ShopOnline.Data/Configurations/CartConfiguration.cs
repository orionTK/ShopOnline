using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopOnline.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopOnline.Data.Configurations
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Carts");
            builder.HasKey(x => x.CartId);
            builder.Property(x => x.CartId).UseIdentityColumn();
            builder.Property(x => x.Price).IsRequired(true);
            builder.Property(x => x.Quantity).IsRequired(true);
            builder.HasOne(x => x.Product).WithMany(x => x.Carts).HasForeignKey(x => x.ProductId);
        }
    }
}
