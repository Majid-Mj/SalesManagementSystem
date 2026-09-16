using Microsoft.EntityFrameworkCore;
using SalesManagementSystem.Data;
using SalesManagementSystem.Models;

namespace SalesManagementSystem.Services;

public class ProductService
{
    private readonly AppDbContext _context;

    public ProductService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<List<Product>> GetAllAsync()
    {
        return await _context.Products
            .FromSqlRaw("SELECT * FROM sp_get_all_products()")
            .ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products
            .FromSqlInterpolated($"SELECT * FROM sp_get_product_by_id({id})")
            .FirstOrDefaultAsync();
    }

    public async Task<string?> AddAsync(Product product)
    {
        bool exists = await _context.Products.AnyAsync(p => p.Code == product.Code);
        if (exists) return "Product code already exists";


        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return null;
    }


    public async Task<string?> UpdateAsync (int id, Product product)
    {
        var existing = await _context.Products.FindAsync(id);
        if (existing == null)
        {
            return "Product Not Found";
        }

        bool codeExists = await _context.Products.AnyAsync(p => p.Code == product.Code && p.Id != id);  
        if (codeExists)
        {
            return "code is Already Exists";
        }

        existing.Name = product.Name;
        existing.Code = product.Code;
        existing.Cost = product.Cost;
        existing.Price = product.Price;

        await _context.SaveChangesAsync();
        return null;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return false;
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }

}
