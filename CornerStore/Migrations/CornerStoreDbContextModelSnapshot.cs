﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CornerStore.Migrations
{
    [DbContext(typeof(CornerStoreDbContext))]
    partial class CornerStoreDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CornerStore.Models.Cashier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Cashiers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstName = "Bluey",
                            LastName = "Heeler"
                        },
                        new
                        {
                            Id = 2,
                            FirstName = "Courage",
                            LastName = "Bagge"
                        },
                        new
                        {
                            Id = 3,
                            FirstName = "Marshal",
                            LastName = "Firefighter"
                        },
                        new
                        {
                            Id = 4,
                            FirstName = "Scruff",
                            LastName = "McGruff"
                        });
                });

            modelBuilder.Entity("CornerStore.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryName = "Food"
                        },
                        new
                        {
                            Id = 2,
                            CategoryName = "Drinks"
                        },
                        new
                        {
                            Id = 3,
                            CategoryName = "Medication"
                        },
                        new
                        {
                            Id = 4,
                            CategoryName = "Occult"
                        },
                        new
                        {
                            Id = 5,
                            CategoryName = "Misc"
                        });
                });

            modelBuilder.Entity("CornerStore.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CashierId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("PaidOnDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("CashierId");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CashierId = 1,
                            PaidOnDate = new DateTime(2025, 1, 17, 16, 45, 23, 430, DateTimeKind.Local).AddTicks(7446)
                        },
                        new
                        {
                            Id = 2,
                            CashierId = 2,
                            PaidOnDate = new DateTime(2025, 1, 17, 16, 45, 23, 430, DateTimeKind.Local).AddTicks(7550)
                        });
                });

            modelBuilder.Entity("CornerStore.Models.OrderProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderProducts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            OrderId = 1,
                            ProductId = 1,
                            Quantity = 5
                        },
                        new
                        {
                            Id = 2,
                            OrderId = 1,
                            ProductId = 3,
                            Quantity = 3
                        },
                        new
                        {
                            Id = 3,
                            OrderId = 2,
                            ProductId = 2,
                            Quantity = 1
                        },
                        new
                        {
                            Id = 4,
                            OrderId = 2,
                            ProductId = 4,
                            Quantity = 2
                        });
                });

            modelBuilder.Entity("CornerStore.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Brand = "Chewie's",
                            CategoryId = 1,
                            Price = 0.50m,
                            ProductName = "Bubblegum (used)"
                        },
                        new
                        {
                            Id = 2,
                            Brand = "Magic Corp",
                            CategoryId = 4,
                            Price = 100.00m,
                            ProductName = "Wand of Head Exploding"
                        },
                        new
                        {
                            Id = 3,
                            Brand = "FizzFizz",
                            CategoryId = 2,
                            Price = 1.50m,
                            ProductName = "Sad Apple Soda"
                        },
                        new
                        {
                            Id = 4,
                            Brand = "Alchemist Co.",
                            CategoryId = 3,
                            Price = 50.00m,
                            ProductName = "Potion of Healing"
                        },
                        new
                        {
                            Id = 5,
                            Brand = "Post-A-Lot",
                            CategoryId = 5,
                            Price = 0.10m,
                            ProductName = "Mail (used)"
                        },
                        new
                        {
                            Id = 6,
                            Brand = "Fluffy Friends",
                            CategoryId = 5,
                            Price = 300.00m,
                            ProductName = "Puppy"
                        },
                        new
                        {
                            Id = 7,
                            Brand = "Theater Co.",
                            CategoryId = 5,
                            Price = 200.00m,
                            ProductName = "Acting Lessons"
                        },
                        new
                        {
                            Id = 8,
                            Brand = "Wedding Wonders",
                            CategoryId = 5,
                            Price = 15.00m,
                            ProductName = "Best Man Speech (used)"
                        });
                });

            modelBuilder.Entity("CornerStore.Models.Order", b =>
                {
                    b.HasOne("CornerStore.Models.Cashier", "Cashier")
                        .WithMany("Orders")
                        .HasForeignKey("CashierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cashier");
                });

            modelBuilder.Entity("CornerStore.Models.OrderProduct", b =>
                {
                    b.HasOne("CornerStore.Models.Order", "Order")
                        .WithMany("OrderProducts")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CornerStore.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("CornerStore.Models.Product", b =>
                {
                    b.HasOne("CornerStore.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("CornerStore.Models.Cashier", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("CornerStore.Models.Order", b =>
                {
                    b.Navigation("OrderProducts");
                });
#pragma warning restore 612, 618
        }
    }
}
