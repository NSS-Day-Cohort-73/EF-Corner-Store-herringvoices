using CornerStore.Models;
using Microsoft.EntityFrameworkCore;

public class CornerStoreDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Cashier> Cashiers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }

    public CornerStoreDbContext(DbContextOptions<CornerStoreDbContext> context)
        : base(context) { }

    //allows us to configure the schema when migrating as well as seed data
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seed Cashiers
        modelBuilder
            .Entity<Cashier>()
            .HasData(
                new Cashier
                {
                    Id = 1,
                    FirstName = "Bluey",
                    LastName = "Heeler",
                },
                new Cashier
                {
                    Id = 2,
                    FirstName = "Courage",
                    LastName = "Bagge",
                },
                new Cashier
                {
                    Id = 3,
                    FirstName = "Marshal",
                    LastName = "Firefighter",
                },
                new Cashier
                {
                    Id = 4,
                    FirstName = "Scruff",
                    LastName = "McGruff",
                }
            );

        // Seed Categories
        modelBuilder
            .Entity<Category>()
            .HasData(
                new Category { Id = 1, CategoryName = "Food" },
                new Category { Id = 2, CategoryName = "Drinks" },
                new Category { Id = 3, CategoryName = "Medication" },
                new Category { Id = 4, CategoryName = "Occult" },
                new Category { Id = 5, CategoryName = "Misc" }
            );

        // Seed Products
        modelBuilder
            .Entity<Product>()
            .HasData(
                new Product
                {
                    Id = 1,
                    ProductName = "Bubblegum (used)",
                    Price = 0.50m,
                    Brand = "Chewie's",
                    CategoryId = 1,
                },
                new Product
                {
                    Id = 2,
                    ProductName = "Wand of Head Exploding",
                    Price = 100.00m,
                    Brand = "Magic Corp",
                    CategoryId = 4,
                },
                new Product
                {
                    Id = 3,
                    ProductName = "Sad Apple Soda",
                    Price = 1.50m,
                    Brand = "FizzFizz",
                    CategoryId = 2,
                },
                new Product
                {
                    Id = 4,
                    ProductName = "Potion of Healing",
                    Price = 50.00m,
                    Brand = "Alchemist Co.",
                    CategoryId = 3,
                },
                new Product
                {
                    Id = 5,
                    ProductName = "Mail (used)",
                    Price = 0.10m,
                    Brand = "Post-A-Lot",
                    CategoryId = 5,
                },
                new Product
                {
                    Id = 6,
                    ProductName = "Puppy",
                    Price = 300.00m,
                    Brand = "Fluffy Friends",
                    CategoryId = 5,
                },
                new Product
                {
                    Id = 7,
                    ProductName = "Acting Lessons",
                    Price = 200.00m,
                    Brand = "Theater Co.",
                    CategoryId = 5,
                },
                new Product
                {
                    Id = 8,
                    ProductName = "Best Man Speech (used)",
                    Price = 15.00m,
                    Brand = "Wedding Wonders",
                    CategoryId = 5,
                }
            );

        // Seed Orders
        modelBuilder
            .Entity<Order>()
            .HasData(
                new Order
                {
                    Id = 1,
                    CashierId = 1,
                    PaidOnDate = DateTime.Now,
                },
                new Order
                {
                    Id = 2,
                    CashierId = 2,
                    PaidOnDate = DateTime.Now,
                }
            );

        // Seed OrderProducts
        modelBuilder
            .Entity<OrderProduct>()
            .HasData(
                new OrderProduct
                {
                    Id = 1,
                    OrderId = 1,
                    ProductId = 1,
                    Quantity = 5,
                }, // 5 Bubblegum (used)
                new OrderProduct
                {
                    Id = 2,
                    OrderId = 1,
                    ProductId = 3,
                    Quantity = 3,
                }, // 3 Sad Apple Soda
                new OrderProduct
                {
                    Id = 3,
                    OrderId = 2,
                    ProductId = 2,
                    Quantity = 1,
                }, // 1 Wand of Head Exploding
                new OrderProduct
                {
                    Id = 4,
                    OrderId = 2,
                    ProductId = 4,
                    Quantity = 2,
                } // 2 Potion of Healing
            );
    }
}
