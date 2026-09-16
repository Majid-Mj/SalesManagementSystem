using Microsoft.EntityFrameworkCore;
using SalesManagementSystem.Data;
using SalesManagementSystem.DTOs;
using SalesManagementSystem.Models;

namespace SalesManagementSystem.Services;

public class CustomerService
{
    private readonly AppDbContext _context;

    public CustomerService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Customer>> GetAllCustomersAsync()
    {
        return await _context.Customers
            .AsNoTracking()
            .ToListAsync();
    }


    public async Task<Customer?> GetCustomerByIdAsync(int id)
    {
        return await _context.Customers
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Customer> CreateCustomerAsync(CustomerDto model)
    {
        var customer = new Customer
        {
            Name = model.Name,
            Code = model.Code,
            Address = model.Address
        };

        _context.Customers.Add(customer);

        await _context.SaveChangesAsync();

        return customer;
    }

    public async Task<bool> CustomerCodeExistsAsync(
        string code,
        int? customerId = null)
    {
        return await _context.Customers
            .AnyAsync(c =>
                c.Code == code &&
                (!customerId.HasValue || c.Id != customerId.Value));
    }

    public async Task<Customer?> UpdateCustomerAsync(
        int id,
        CustomerDto model)
    {
        var customer = await _context.Customers
            .FirstOrDefaultAsync(c => c.Id == id);

        if (customer == null)
        {
            return null;
        }

        customer.Name = model.Name;
        customer.Code = model.Code;
        customer.Address = model.Address;

        await _context.SaveChangesAsync();

        return customer;
    }


    public async Task<bool> DeleteCustomerAsync(int id)
    {
        var customer = await _context.Customers
            .FirstOrDefaultAsync(c => c.Id == id);

        if (customer == null)
        {
            return false;
        }

        _context.Customers.Remove(customer);

        await _context.SaveChangesAsync();

        return true;
    }


}