using System.Text.Json.Serialization;
using CornerStore.DTOs;
using CornerStore.Models;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// allows passing datetimes without time zone data
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// allows our api endpoints to access the database through Entity Framework Core and provides dummy value for testing
builder.Services.AddNpgsql<CornerStoreDbContext>(
    builder.Configuration["CornerStoreDbConnectionString"] ?? "testing"
);

// Set the JSON serializer options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//endpoints go here

//Cashier endpoints
//Get a cashier by id
app.MapGet(
    "/cashiers/{id}",
    async (int id, CornerStoreDbContext db) =>
    {
        if (!await db.Cashiers.AnyAsync(c => c.Id == id))
        {
            return Results.NotFound();
        }
        var cashier = db
            .Cashiers.Include(c => c.Orders)
            .ThenInclude(o => o.OrderProducts)
            .ThenInclude(op => op.Product)
            .Where(c => c.Id == id)
            .Select(c => new CashierDTO
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Orders = c
                    .Orders.Select(o => new OrderDTO
                    {
                        Id = o.Id,
                        CashierId = o.CashierId,
                        PaidOnDate = o.PaidOnDate ?? DateTime.Now,
                        OrderProducts = o
                            .OrderProducts.Select(op => new OrderProductDTO
                            {
                                Id = op.Id,
                                OrderId = op.OrderId,
                                ProductId = op.ProductId,
                                Quantity = op.Quantity,
                                Product = new ProductDTO
                                {
                                    Id = op.Product.Id,
                                    ProductName = op.Product.ProductName,
                                    Price = op.Product.Price,
                                    CategoryId = op.Product.CategoryId,
                                    Category = new CategoryDTO
                                    {
                                        Id = op.Product.Category.Id,
                                        CategoryName = op.Product.Category.CategoryName,
                                    },
                                },
                            })
                            .ToList(),
                    })
                    .ToList(),
            })
            .SingleOrDefault();

        return Results.Ok(cashier);
    }
);

//Add a cashier
app.MapPost(
    "/cashiers",
    (Cashier cashier, CornerStoreDbContext db) =>
    {
        db.Cashiers.Add(cashier);
        db.SaveChanges();
        return Results.Created($"/cashiers/{cashier.Id}", cashier);
    }
);

//Get products with categories. Include optional search query string parameter
app.MapGet(
    "/products",
    (CornerStoreDbContext db, string? search) =>
    {
        var products = db
            .Products.Include(p => p.Category)
            .Where(p =>
                search == null
                || p.ProductName.ToLower().Contains(search.ToLower())
                || p.Category.CategoryName.ToLower().Contains(search.ToLower())
            )
            .Select(p => new ProductDTO
            {
                Id = p.Id,
                ProductName = p.ProductName,
                Price = p.Price,
                Brand = p.Brand,
                CategoryId = p.CategoryId,
                Category = new CategoryDTO
                {
                    Id = p.Category.Id,
                    CategoryName = p.Category.CategoryName,
                },
            })
            .ToList();
        if (products.Count == 0)
        {
            return Results.NotFound();
        }
        return Results.Ok(products);
    }
);

//Add a product
app.MapPost(
    "/products",
    (Product product, CornerStoreDbContext db) =>
    {
        db.Products.Add(product);
        db.SaveChanges();

        return Results.Created($"/products/{product.Id}", product);
    }
);

//Update a product
app.MapPut(
    "/products/{id}",
    (int id, Product product, CornerStoreDbContext db) =>
    {
        // Check if ID in URL matches ID in body
        if (id != product.Id)
        {
            return Results.BadRequest();
        }

        // Retrieve existing product from the database
        var existingProduct = db.Products.Find(id);
        if (existingProduct == null)
        {
            return Results.NotFound();
        }

        // Update the existing entity's values
        db.Entry(existingProduct).CurrentValues.SetValues(product);

        // Save changes to the database
        db.SaveChanges();

        // Return 204 No Content on success
        return Results.NoContent();
    }
);

//Orders Endpoints

//Get an order details including cashier, order products, and products (with categories) by order id
app.MapGet(
    "/orders/{id}",
    (CornerStoreDbContext db, int id) =>
    {
        var order = db
            .Orders.Where(o => o.Id == id)
            .Select(o => new OrderDTO
            {
                Id = o.Id,
                Cashier =
                    o.Cashier == null
                        ? null
                        : new CashierDTO
                        {
                            Id = o.Cashier.Id,
                            FirstName = o.Cashier.FirstName,
                            LastName = o.Cashier.LastName,
                        },
                PaidOnDate = o.PaidOnDate,
                OrderProducts = o
                    .OrderProducts.Select(op => new OrderProductDTO
                    {
                        Id = op.Id,
                        OrderId = op.OrderId,
                        ProductId = op.ProductId,
                        Quantity = op.Quantity,
                        Product =
                            op.Product == null
                                ? null
                                : new ProductDTO
                                {
                                    Id = op.Product.Id,
                                    ProductName = op.Product.ProductName,
                                    Price = op.Product.Price,
                                    CategoryId = op.Product.CategoryId,
                                    Category =
                                        op.Product.Category == null
                                            ? null
                                            : new CategoryDTO
                                            {
                                                Id = op.Product.Category.Id,
                                                CategoryName = op.Product.Category.CategoryName,
                                            },
                                },
                    })
                    .ToList(),
            })
            .FirstOrDefault();

        return order != null ? Results.Ok(order) : Results.NotFound();
    }
);

//Get all orders. Include optional query string parameter to filter by a particular date in YYY-MM-DD format
app.MapGet(
    "/orders",
    (CornerStoreDbContext db, DateTime? orderDate) =>
    {
        //Capture the query in a variable
        IQueryable<Order> ordersQuery = db
            .Orders.Include(o => o.OrderProducts)
            .ThenInclude(op => op.Product)
            .ThenInclude(p => p.Category)
            .Include(o => o.Cashier);
        //If the date query parameter is present, filter the ordersQuery by the date
        if (orderDate.HasValue)
        {
            ordersQuery = ordersQuery.Where(o =>
                o.PaidOnDate.HasValue && o.PaidOnDate.Value.Date == orderDate.Value.Date
            );
        }

        //Map the ordersQuery to OrderDTO
        var orders = ordersQuery
            .Select(o => new OrderDTO
            {
                Id = o.Id,
                Cashier = new CashierDTO
                {
                    Id = o.Cashier.Id,
                    FirstName = o.Cashier.FirstName,
                    LastName = o.Cashier.LastName,
                },
                PaidOnDate = o.PaidOnDate,
                OrderProducts = o
                    .OrderProducts.Select(op => new OrderProductDTO
                    {
                        Id = op.Id,
                        OrderId = op.OrderId,
                        ProductId = op.ProductId,
                        Quantity = op.Quantity,
                        Product = new ProductDTO
                        {
                            Id = op.Product.Id,
                            ProductName = op.Product.ProductName,
                            Price = op.Product.Price,
                            CategoryId = op.Product.CategoryId,
                            Category = new CategoryDTO
                            {
                                Id = op.Product.Category.Id,
                                CategoryName = op.Product.Category.CategoryName,
                            },
                        },
                    })
                    .ToList(),
            })
            .ToList();

        return orders != null ? Results.Ok(orders) : Results.NotFound();
    }
);

//Delete an order by id
app.MapDelete(
    "/orders/{id}",
    (int id, CornerStoreDbContext db) =>
    {
        var order = db.Orders.Find(id);
        if (order == null)
        {
            return Results.NotFound();
        }
        db.Orders.Remove(order);
        db.SaveChanges();
        return Results.NoContent();
    }
);

//Create an order with order products
//The request body should contain an Order object with a list of OrderProduct objects
app.MapPost(
    "/orders",
    (Order order, CornerStoreDbContext db) =>
    {
        if (order.OrderProducts != null)
        {
            foreach (var orderProduct in order.OrderProducts)
            {
                // Load the existing Product from the database
                var existingProduct = db.Products.SingleOrDefault(p =>
                    p.Id == orderProduct.ProductId
                );

                if (existingProduct == null)
                {
                    return Results.BadRequest(
                        $"Product with ID {orderProduct.ProductId} does not exist."
                    );
                }

                // Associate the existing Product with the OrderProduct
                orderProduct.Product = existingProduct;

                // Set the Order ID for the OrderProduct
                orderProduct.OrderId = order.Id;
            }
        }

        // Add the order to the database
        db.Orders.Add(order);

        // Save the changes
        db.SaveChanges();

        // Reload the order to include navigation properties
        var createdOrder = db
            .Orders.Include(o => o.OrderProducts)
            .ThenInclude(op => op.Product)
            .First(o => o.Id == order.Id);

        return Results.Created($"/orders/{createdOrder.Id}", createdOrder);
    }
);

app.Run();

//don't move or change this!
public partial class Program { }
