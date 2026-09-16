using Microsoft.EntityFrameworkCore;
using SalesManagementSystem.Application.Common;
using SalesManagementSystem.Infrastructure.Data;
using SalesManagementSystem.Application.DTOs;
using SalesManagementSystem.Application.Interfaces;
using SalesManagementSystem.Domain.Entities;

namespace SalesManagementSystem.Application.Services;

public class ProductService : IProductService
{
    private readonly AppDbContext _context;

    public ProductService(AppDbContext context) => _context = context;

    public async Task<ApiResponse<List<Product>>> GetAllAsync()
    {
        var products = await _context.Products
            .FromSqlRaw("SELECT * FROM sp_get_all_products()")
            .AsNoTracking()
            .ToListAsync();

        return ApiResponse<List<Product>>.SuccessResponse(products);
    }

    public async Task<ApiResponse<Product>> GetByIdAsync(int id)
    {
        var product = await _context.Products
            .FromSqlInterpolated($"SELECT * FROM sp_get_product_by_id({id})")
            .AsNoTracking()
            .FirstOrDefaultAsync();

        return product is null
            ? ApiResponse<Product>.NotFound("Product not found")
            : ApiResponse<Product>.SuccessResponse(product);
    }

    public async Task<ApiResponse<Product>> AddAsync(ProductDto dto)
    {
        if (await _context.Products.AnyAsync(p => p.Code == dto.Code))
            return ApiResponse<Product>.Conflict("Product code already exists");

        var product = new Product
        {
            Name = dto.Name,
            Code = dto.Code,
            Cost = dto.Cost,
            Price = dto.Price
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return ApiResponse<Product>.Created(product, "Product added successfully");
    }

    public async Task<ApiResponse> UpdateAsync(int id, ProductDto dto)
    {
        var existing = await _context.Products.FindAsync(id);
        if (existing is null)
            return ApiResponse.NotFound("Product not found");

        if (existing.Code != dto.Code && await _context.Products.AnyAsync(p => p.Code == dto.Code))
            return ApiResponse.Conflict("Product code already exists");

        existing.Name = dto.Name;
        existing.Code = dto.Code;
        existing.Cost = dto.Cost;
        existing.Price = dto.Price;

        await _context.SaveChangesAsync();
        return ApiResponse.SuccessResponse("Product updated successfully");
    }

    public async Task<ApiResponse> DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product is null)
            return ApiResponse.NotFound("Product not found");

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return ApiResponse.SuccessResponse("Product deleted successfully");
    }
}
