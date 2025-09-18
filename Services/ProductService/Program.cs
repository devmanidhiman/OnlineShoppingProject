using Models;
using ProductService.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ProductService.Services.ProductService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/products",async (ProductService.Services.ProductService productService) =>
{
    return Results.Ok(await productService.GetAllProductsAsync());
}).WithName("GetProducts").WithTags("Products");

app.MapGet("/products/{id}", async (int id, ProductService.Services.ProductService productService) =>
{
    var product = await productService.GetProductByIdAsync(id);
    return product is not null ? Results.Ok(product) : Results.NotFound();
}).WithName("GetProductById").WithTags("Products");

app.MapPost("/products", async (Product product, ProductService.Services.ProductService productService) =>
{
    await productService.AddProductAsync(product);
    return Results.Created($"/products/{product.Id}", product);
}).WithName("AddProduct").WithTags("Products");

app.MapPut("/products/{id}", async (int id, Product updatedProduct, ProductService.Services.ProductService productService) =>
{
    var result = await productService.UpdateProductAsync(id, updatedProduct);
    return result ? Results.NoContent() : Results.NotFound();
}).WithName("UpdateProduct").WithTags("Products");

app.MapDelete("/products/{id}", async (int id, ProductService.Services.ProductService productService) =>
{
    var result = await productService.DeleteProductAsync(id);
    return result ? Results.NoContent() : Results.NotFound();
}).WithName("DeleteProduct").WithTags("Products");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ProductDbContext>();

    if (!db.Products.Any())
    {
        db.Products.AddRange(new List<Product>
        {
            new Product { Name = "Laptop", Price = 999.99M, Description = "A high-performance laptop.", Stock = 10, Category = "Electronics" },
            new Product { Name = "Smartphone", Price = 499.99M, Description = "A latest model smartphone.", Stock = 25, Category = "Electronics" },
            new Product { Name = "Headphones", Price = 79.99M, Description = "Noise-cancelling headphones.", Stock = 50, Category = "Accessories" }
        });
        db.SaveChanges();
    }
}

app.Run();
