using SalesManagementSystem.Application.Common;
using SalesManagementSystem.Application.DTOs;
using SalesManagementSystem.Domain.Entities;

namespace SalesManagementSystem.Application.Interfaces;

public interface IProductService
{
    Task<ApiResponse<List<Product>>> GetAllAsync();
    Task<ApiResponse<Product>> GetByIdAsync(int id);
    Task<ApiResponse<Product>> AddAsync(ProductDto dto);
    Task<ApiResponse> UpdateAsync(int id, ProductDto dto);
    Task<ApiResponse> DeleteAsync(int id);
}
