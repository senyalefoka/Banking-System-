using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts");

            builder.HasKey(p => p.code);
            builder.Property(p => p.code)
                .HasColumnName("Code")
                .ValueGeneratedNever();

            builder.Property(p => p.AccountNumber)
                .IsRequired()
                .HasMaxLength(12)
            .HasJsonPropertyName("AccountNumber");

            builder.Property(p => p.Balance)
                .HasMaxLength(12)
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasColumnName("Balance");

            builder.Property(p => p.PersonalId).IsRequired()
                .HasColumnName("PersonalId");



            builder.HasMany(a => a.Transactions)
                .WithOne(t => t.Account)
                .HasForeignKey(t => t.AccountId);

            builder.HasIndex(a => a.AccountNumber).IsUnique();
            builder.HasIndex(a => a.PersonalId).IsUnique();











        }
    }

}
