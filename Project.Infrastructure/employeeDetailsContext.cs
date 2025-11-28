using Microsoft.EntityFrameworkCore;
using Project.Domain;
using Project.Domain.Entities;

//using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure
{
    public class employeeDetailsContext: DbContext
    {

public employeeDetailsContext(DbContextOptions<employeeDetailsContext> options):base(options)
        {


        }


        public DbSet<PersonalDetail> PersonalDetails { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
        public DbSet<Login> Logins { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {
            base.OnModelCreating(modelBuilder);
            // This tells EF to look for configuration files
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


        }
    }
}
