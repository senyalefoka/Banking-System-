using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<employeeDetailsContext>

    {
   
        employeeDetailsContext IDesignTimeDbContextFactory<employeeDetailsContext>.CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<employeeDetailsContext>();

            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=DonnyPractice;Trusted_Connection=true;MultipleActiveResultSets=true;TrustServerCertificate=true");


            return new employeeDetailsContext(optionsBuilder.Options);



        }
    }
}
