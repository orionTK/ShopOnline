using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopOnline.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopOnline.Data.Configurations
{
    public class SlideConfiguration : IEntityTypeConfiguration<Slide>
    {
        public void Configure(EntityTypeBuilder<Slide> builder)
        {
            builder.ToTable("Slides");
            builder.HasKey(x => x.SlideId);

            builder.Property(x => x.SlideId).UseIdentityColumn();

            builder.Property(x => x.SlideName).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Url).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Image);
            builder.Property(x => x.SortOrder);
            builder.Property(x => x.Status);

            builder.Property(x => x.Image).HasMaxLength(200).IsRequired();
        }
    }
}
