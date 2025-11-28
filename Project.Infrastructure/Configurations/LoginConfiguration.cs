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
    public  class LoginConfiguration: IEntityTypeConfiguration<Login>
        {
        public void Configure(EntityTypeBuilder<Login> builder)
        {
            builder.ToTable<Login>(nameof(Login));
            builder.HasKey(p => p.loginId);
            builder.Property(p => p.loginId).HasColumnName("LoginId").ValueGeneratedNever();
            builder.Property(p => p.Username).
                IsRequired()
                .HasMaxLength(50)
                .HasColumnName("Username");
            builder.Property(p => p.Password).
               IsRequired()
               .HasMaxLength(100)
               .HasColumnName("Password");
            builder.Property(p => p.Email).
               IsRequired()
               .HasMaxLength(100)
               .HasColumnName("Email");
        }
    
    }
}
