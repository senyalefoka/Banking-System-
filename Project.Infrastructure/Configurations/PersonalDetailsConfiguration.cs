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
    public class PersonalDetailsConfiguration : IEntityTypeConfiguration<PersonalDetail>
    {
        public void Configure(EntityTypeBuilder<PersonalDetail> builder)
        {
            builder.ToTable(nameof(PersonalDetail));

            // Primary key
            builder.HasKey(p => p.Code);
            builder.Property(p => p.Code)
                .HasColumnName("Code")
                .ValueGeneratedNever();

            // Column properties
            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("FirstName");

            builder.Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("LastName");

            builder.Property(p => p.IDNumber)
                .IsRequired()
                .HasMaxLength(13)
                .HasColumnName("IDNumber");

            // FIXED: Remove spaces from column name or use proper SQL brackets
            builder.Property(p => p.DateOfBirth)
                .IsRequired()
                .HasColumnName("DateOfBirth"); // ✅ No spaces - recommended

            // Alternative if you MUST keep spaces in database:
            // .HasColumnName("[Date of Birth]"); // ✅ With SQL brackets
        }
    }

}
