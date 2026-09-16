using Microsoft.EntityFrameworkCore;
using SalesManagementSystem.Application.Common;
using SalesManagementSystem.Infrastructure.Data;
using SalesManagementSystem.Application.DTOs;
using SalesManagementSystem.Application.Interfaces;
using SalesManagementSystem.Domain.Entities;

namespace SalesManagementSystem.Application.Services;

public class CustomerService : ICustomerService
{
    private readonly AppDbContext _context;

    public CustomerService(AppDbContext context) => _context = context;

    public async Task<ApiResponse<List<Customer>>> GetAllCustomersAsync() =>
        ApiResponse<List<Customer>>.SuccessResponse(await _context.Customers.AsNoTracking().ToListAsync());

    public async Task<ApiResponse<Customer>> GetCustomerByIdAsync(int id)
    {
        var customer = await _context.Customers.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        return customer is null
            ? ApiResponse<Customer>.NotFound("Customer not found")
            : ApiResponse<Customer>.SuccessResponse(customer);
    }

    public async Task<ApiResponse<Customer>> CreateCustomerAsync(CustomerDto model)
    {
        if (await _context.Customers.AnyAsync(c => c.Code == model.Code))
            return ApiResponse<Customer>.Conflict("Customer code already exists");

        var customer = new Customer
        {
            Name = model.Name,
            Code = model.Code,
            Address = model.Address
        };

        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();

        return ApiResponse<Customer>.Created(customer, "Customer created successfully");
    }

    public async Task<ApiResponse<Customer>> UpdateCustomerAsync(int id, CustomerDto model)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
        if (customer is null)
            return ApiResponse<Customer>.NotFound("Customer not found");

        if (customer.Code != model.Code && await _context.Customers.AnyAsync(c => c.Code == model.Code))
            return ApiResponse<Customer>.Conflict("Customer code already exists");

        customer.Name = model.Name;
        customer.Code = model.Code;
        customer.Address = model.Address;

        await _context.SaveChangesAsync();
        return ApiResponse<Customer>.SuccessResponse(customer, "Customer updated successfully");
    }

    public async Task<ApiResponse> DeleteCustomerAsync(int id)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
        if (customer is null)
            return ApiResponse.NotFound("Customer not found");

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();

        return ApiResponse.SuccessResponse("Customer deleted successfully");
    }
}
