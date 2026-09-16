using SalesManagementSystem.Application.Common;
using SalesManagementSystem.Application.DTOs;
using SalesManagementSystem.Domain.Entities;

namespace SalesManagementSystem.Application.Interfaces;

public interface ICustomerService
{
    Task<ApiResponse<List<Customer>>> GetAllCustomersAsync();
    Task<ApiResponse<Customer>> GetCustomerByIdAsync(int id);
    Task<ApiResponse<Customer>> CreateCustomerAsync(CustomerDto model);
    Task<ApiResponse<Customer>> UpdateCustomerAsync(int id, CustomerDto model);
    Task<ApiResponse> DeleteCustomerAsync(int id);
}
