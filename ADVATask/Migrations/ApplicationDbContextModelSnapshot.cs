﻿// <auto-generated />
using ADVATask.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ADVATask.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ADVATask.Data.Department", b =>
                {
                    b.Property<int>("DepartmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DepartmentID"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DepartmentID");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("ADVATask.Data.Employee", b =>
                {
                    b.Property<int>("EmployeeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeID"), 1L, 1);

                    b.Property<int>("DepartmentID")
                        .HasColumnType("int");

                    b.Property<bool>("IsManger")
                        .HasColumnType("bit");

                    b.Property<int>("MangerID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Salary")
                        .HasColumnType("float");

                    b.HasKey("EmployeeID");

                    b.HasIndex("DepartmentID");

                    b.HasIndex("MangerID");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("ADVATask.Data.Employee", b =>
                {
                    b.HasOne("ADVATask.Data.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ADVATask.Data.Employee", "Manger")
                        .WithMany("Employees")
                        .HasForeignKey("MangerID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Manger");
                });

            modelBuilder.Entity("ADVATask.Data.Department", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("ADVATask.Data.Employee", b =>
                {
                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}
