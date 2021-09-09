using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopOnline.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopOnline.Data.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions");

            builder.HasKey(x => x.TransactionId);

            builder.Property(x => x.TransactionId).UseIdentityColumn();
            builder.Property(x => x.TransactionDate);

            builder.Property(x => x.ExternalTransactionId);

            builder.Property(x => x.Amount).IsRequired();

            builder.Property(x => x.Fee).IsRequired();
            builder.Property(x => x.Result);

            builder.Property(x => x.Message);

            builder.Property(x => x.Status);
            builder.Property(x => x.Provider);

            builder.HasOne(x => x.User).WithMany(x => x.Transactions).HasForeignKey(x => x.UserId);

        }
    }
}
