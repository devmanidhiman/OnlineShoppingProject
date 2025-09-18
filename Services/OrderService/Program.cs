using Models;
using OrderService.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<OrderService.Services.OrderService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/orders", async (OrderService.Services.OrderService orderService) =>
    Results.Ok(await orderService.GetAllOrdersAsync()))
    .WithName("GetOrders").WithTags("Orders");

app.MapGet("/orders/{id}", async (int id, OrderService.Services.OrderService orderService) =>
{
    var order = await orderService.GetOrderByIdAsync(id);
    return order is not null ? Results.Ok(order) : Results.NotFound();
}).WithName("GetOrderById").WithTags("Orders");

app.MapPost("/orders", async (Order order, OrderService.Services.OrderService orderService) =>
{
    await orderService.AddOrderAsync(order);
    return Results.Created($"/orders/{order.Id}", order);
}).WithName("AddOrder").WithTags("Orders");

app.MapPut("/orders/{id}", async (int id, Order updatedOrder, OrderService.Services.OrderService orderService) =>
{
    var result = await orderService.UpdateOrderAsync(id, updatedOrder);
    return result ? Results.NoContent() : Results.NotFound();
}).WithName("UpdateOrder").WithTags("Orders");

app.MapDelete("/orders/{id}", async (int id, OrderService.Services.OrderService orderService) =>
{
    var result = await orderService.DeleteOrderAsync(id);
    return result ? Results.NoContent() : Results.NotFound();
}).WithName("DeleteOrder").WithTags("Orders");

app.Run();