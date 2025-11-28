using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.Entities;
using System;
using Project.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transactions>
    {
        public void Configure(EntityTypeBuilder<Transactions> builder)
        {
            builder.ToTable("Transactions");
            builder.HasKey(p => p.code);

            builder.Property(p => p.code)
                .HasColumnName("code")
                .ValueGeneratedNever();

            builder.Property(p => p.AccountId)
                .IsRequired()
                .HasColumnName("AccountId"); // REMOVE HasMaxLength(12)

            builder.Property(p => p.Outstanding_Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasColumnName("Outstanding_Amount");

            builder.Property(p => p.TransactionDate)
                .IsRequired()
                .HasColumnName("TransactionDate");

            builder.Property(p => p.TransactionType)
                .IsRequired()
                .HasColumnName("TransactionType");

            builder.Property(p => p.Descriptiion)
                .IsRequired()
                .HasColumnName("Descriptiion");

            builder.HasOne(t => t.Account)
                .WithMany(a => a.Transactions)
                .HasForeignKey(t => t.AccountId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
