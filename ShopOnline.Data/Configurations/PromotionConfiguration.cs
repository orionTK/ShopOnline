using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopOnline.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopOnline.Data.Configurations
{
    public class PromotionConfiguration : IEntityTypeConfiguration<Promotion>
    {
        public void Configure(EntityTypeBuilder<Promotion> builder)
        {
            builder.ToTable("Promotions");

            builder.HasKey(x => x.PromotionId);
            builder.Property(x => x.PromotionId).UseIdentityColumn();
            builder.Property(x => x.PromotionName).IsRequired().HasMaxLength(200);
            builder.Property(x => x.FromDate).IsRequired();
            builder.Property(x => x.ToDate).IsRequired();
            builder.Property(x => x.ApplyForAll);
            builder.Property(x => x.DiscountPercent);
            builder.Property(x => x.DiscountAmount).IsRequired();

        }
    }
}
