﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuarterlySalesApp.Data;

#nullable disable

namespace QuarterlySalesApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240726155840_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("QuarterlySalesApp.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"));

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfHire")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("ManagerId")
                        .HasColumnType("int");

                    b.HasKey("EmployeeId");

                    b.HasIndex("ManagerId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            EmployeeId = 1,
                            DOB = new DateTime(1956, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateOfHire = new DateTime(1995, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Ada",
                            LastName = "Lovelace"
                        },
                        new
                        {
                            EmployeeId = 2,
                            DOB = new DateTime(1960, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateOfHire = new DateTime(2000, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Alan",
                            LastName = "Turing",
                            ManagerId = 1
                        });
                });

            modelBuilder.Entity("QuarterlySalesApp.Models.SalesData", b =>
                {
                    b.Property<int>("SalesDataId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SalesDataId"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("Quarter")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("SalesDataId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("QuarterlySalesApp.Models.Employee", b =>
                {
                    b.HasOne("QuarterlySalesApp.Models.Employee", "Manager")
                        .WithMany("Subordinates")
                        .HasForeignKey("ManagerId");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("QuarterlySalesApp.Models.SalesData", b =>
                {
                    b.HasOne("QuarterlySalesApp.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("QuarterlySalesApp.Models.Employee", b =>
                {
                    b.Navigation("Subordinates");
                });
#pragma warning restore 612, 618
        }
    }
}
