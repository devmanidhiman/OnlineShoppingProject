using Models;
using OrderService.Data;
using Microsoft.EntityFrameworkCore;

namespace OrderService.Services
{
    public class OrderService
    {
        private readonly OrderDbContext _context;

        public OrderService(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task AddOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateOrderAsync(int id, Order updatedOrder)
        {
            var existingOrder = await _context.Orders.FindAsync(id);
            if (existingOrder is null) return false;

            existingOrder.Id = updatedOrder.Id;
            existingOrder.UserId = updatedOrder.UserId;
            existingOrder.OrderDate = updatedOrder.OrderDate;
            existingOrder.TotalAmount = updatedOrder.TotalAmount;
            existingOrder.Status = updatedOrder.Status;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order is null) return false;

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}