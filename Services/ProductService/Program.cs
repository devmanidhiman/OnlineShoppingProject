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

app.MapGet("/products", (ProductService.Services.ProductService productService) =>
{
    return Results.Ok(productService.GetAllProducts());
}).WithName("GetProducts").WithTags("Products");

app.MapGet("/products/{id}", (int id, ProductService.Services.ProductService productService) =>
{
    var product = productService.GetProductById(id);
    return product is not null ? Results.Ok(product) : Results.NotFound();
}).WithName("GetProductById").WithTags("Products");

app.MapPost("/products", (Product product, ProductService.Services.ProductService productService) =>
{
    productService.AddProduct(product);
    return Results.Created($"/products/{product.Id}", product);
}).WithName("AddProduct").WithTags("Products");

app.MapPut("/products/{id}", (int id, Product updatedProduct, ProductService.Services.ProductService productService) =>
{
    var result = productService.UpdateProduct(id, updatedProduct);
    return result ? Results.NoContent() : Results.NotFound();
}).WithName("UpdateProduct").WithTags("Products");

app.MapDelete("/products/{id}", (int id, ProductService.Services.ProductService productService) =>
{
    var result = productService.DeleteProduct(id);
    return result ? Results.NoContent() : Results.NotFound();
}).WithName("DeleteProduct").WithTags("Products");

app.Run();
