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
        public IEnumerable<Product> GetAllProducts() => _context.Products.AsNoTracking().ToList();

        public Product? GetProductById(int id) => _context.Products.AsNoTracking()
                                                        .FirstOrDefault(p => p.Id == id);
        public void AddProduct(Product product)
        {
            product.Id = _context.Products.Any() ? _context.Products.Max(p => p.Id) + 1 : 1;
            product.CreatedAt = DateTime.UtcNow;
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public bool UpdateProduct(int id, Product updatedproduct)
        {
            var product = GetProductById(id);
            if (product is null) return false;
            product.Name = updatedproduct.Name;
            product.Price = updatedproduct.Price;
            product.Description = updatedproduct.Description;
            product.Stock = updatedproduct.Stock;
            product.Category = updatedproduct.Category;
            _context.SaveChanges();
            return true;
        }

        public bool DeleteProduct(int id)
        {
            var product = GetProductById(id);
            if (product is null) return false;
            _context.Products.Remove(product);
            _context.SaveChanges();
            return true;
        }


    }
}