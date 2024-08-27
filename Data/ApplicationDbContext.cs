using Microsoft.EntityFrameworkCore;
using QuarterlySalesApp.Models;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace QuarterlySalesApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<SalesData> Sales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    EmployeeId = 1,
                    FirstName = "Ada",
                    LastName = "Lovelace",
                    DOB = new DateTime(1956, 12, 10),
                    DateOfHire = new DateTime(1995, 1, 1),
                    ManagerId = null
                },
                new Employee
                {
                    EmployeeId = 2,
                    FirstName = "Alan",
                    LastName = "Turing",
                    DOB = new DateTime(1960, 6, 23),
                    DateOfHire = new DateTime(2000, 4, 1),
                    ManagerId = 1
                }
            );

            modelBuilder.Entity<SalesData>()
                .Property(s => s.Amount)
                .HasColumnType("decimal(18,2)");
        }
    }
}
