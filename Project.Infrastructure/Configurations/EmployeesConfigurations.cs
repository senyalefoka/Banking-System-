using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain;
//using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.Configurations
{
    public class EmployeesConfigurations : IEntityTypeConfiguration<EmployeeDetails>

    {

     public void Configure(EntityTypeBuilder<EmployeeDetails> builder)
        {
            builder.ToTable<EmployeeDetails>(nameof(EmployeeDetails));
            builder.HasKey(p=>p.emNo);
            builder.Property(p=>p.emNo).HasColumnName("EmployeeNumber").ValueGeneratedNever();

           
            builder.Property(p => p.empName).
                IsRequired()
                .HasMaxLength(50)
                .HasColumnName("EmployeeName");

            builder.Property(p => p.empSurname).
               IsRequired()
               .HasMaxLength(50)
               .HasColumnName("EmployeeSurName");

            builder.Property(p => p.empIdNum).
               IsRequired()
               .HasMaxLength(13)
               .HasColumnName("EmployeeIDNumber");

        }
    }
}
