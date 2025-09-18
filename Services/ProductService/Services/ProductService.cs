using Models;
using ProductService.Data;
using Microsoft.EntityFrameworkCore;

namespace ProductService.Services
{
    public class ProductService
    {
        private readonly ProductDbContext _context;
        public ProductService(ProductDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Product>> GetAllProductsAsync() => await _context.Products.AsNoTracking().ToListAsync();

        public async Task<Product?> GetProductByIdAsync(int id) => await _context.Products.AsNoTracking()
                                                        .FirstOrDefaultAsync(p => p.Id == id);
        public async Task AddProductAsync(Product product)
        {
            product.Id = _context.Products.Any() ? _context.Products.Max(p => p.Id) + 1 : 1;
            product.CreatedAt = DateTime.UtcNow;
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateProductAsync(int id, Product updatedproduct)
        {
            var product = await GetProductByIdAsync(id);
            if (product is null) return false;
            product.Name = updatedproduct.Name;
            product.Price = updatedproduct.Price;
            product.Description = updatedproduct.Description;
            product.Stock = updatedproduct.Stock;
            product.Category = updatedproduct.Category;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await GetProductByIdAsync(id);
            if (product is null) return false;
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }


    }
}