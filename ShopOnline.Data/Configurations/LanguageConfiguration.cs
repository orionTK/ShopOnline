using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopOnline.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopOnline.Data.Configurations
{
    public class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.ToTable("Languages");

            builder.HasKey(x => x.LanguageId);

            builder.Property(x => x.LanguageId).IsRequired().IsUnicode(false).HasMaxLength(5);

            builder.Property(x => x.LanguageName).IsRequired().HasMaxLength(20);
            builder.Property(x => x.IsDefault).IsRequired();
        }
    }
}
