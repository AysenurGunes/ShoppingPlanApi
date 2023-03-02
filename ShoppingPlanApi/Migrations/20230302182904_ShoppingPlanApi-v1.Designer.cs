﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShoppingPlanApi.DataAccess;

#nullable disable

namespace ShoppingPlanApi.Migrations
{
    [DbContext(typeof(ShoppingPlanDbContext))]
    [Migration("20230302182904_ShoppingPlanApi-v1")]
    partial class ShoppingPlanApiv1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ShoppingPlanApi.Models.Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryID"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryID");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("CategoryID"));

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("ShoppingPlanApi.Models.Measurement", b =>
                {
                    b.Property<int>("MeasurementID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MeasurementID"));

                    b.Property<string>("MeasurementName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MeasurementID");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("MeasurementID"));

                    b.ToTable("Measurements");
                });

            modelBuilder.Entity("ShoppingPlanApi.Models.Product", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductID"));

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductID");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("ProductID"));

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ShoppingPlanApi.Models.Role", b =>
                {
                    b.Property<int>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleID"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleID");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("RoleID"));

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("ShoppingPlanApi.Models.ShoppingList", b =>
                {
                    b.Property<int>("ShoppingListID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ShoppingListID"));

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2023, 3, 2, 18, 29, 4, 27, DateTimeKind.Utc).AddTicks(4715));

                    b.Property<int>("CreatedUserID")
                        .HasColumnType("int");

                    b.Property<bool>("Done")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime>("DoneDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShoppingListName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ShoppingListID");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("ShoppingListID"));

                    b.HasIndex("CategoryID");

                    b.HasIndex("UserID");

                    b.ToTable("ShoppingLists");
                });

            modelBuilder.Entity("ShoppingPlanApi.Models.ShoppingListDetail", b =>
                {
                    b.Property<int>("ShoppingListDetailID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ShoppingListDetailID"));

                    b.Property<bool>("Done")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int>("MeasurementID")
                        .HasColumnType("int");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductID")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("ShoppingListID")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ShoppingListDetailID");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("ShoppingListDetailID"));

                    b.HasIndex("MeasurementID");

                    b.HasIndex("ProductID");

                    b.HasIndex("ShoppingListID");

                    b.ToTable("ShoppingListDetails");
                });

            modelBuilder.Entity("ShoppingPlanApi.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleID")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UserID");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("UserID"));

                    b.HasIndex("RoleID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ShoppingPlanApi.Models.ShoppingList", b =>
                {
                    b.HasOne("ShoppingPlanApi.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShoppingPlanApi.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ShoppingPlanApi.Models.ShoppingListDetail", b =>
                {
                    b.HasOne("ShoppingPlanApi.Models.Measurement", "Measurement")
                        .WithMany()
                        .HasForeignKey("MeasurementID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShoppingPlanApi.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShoppingPlanApi.Models.ShoppingList", "ShoppingList")
                        .WithMany()
                        .HasForeignKey("ShoppingListID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Measurement");

                    b.Navigation("Product");

                    b.Navigation("ShoppingList");
                });

            modelBuilder.Entity("ShoppingPlanApi.Models.User", b =>
                {
                    b.HasOne("ShoppingPlanApi.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });
#pragma warning restore 612, 618
        }
    }
}
