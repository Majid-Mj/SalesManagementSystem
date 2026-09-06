using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesManagementSystem.DTOs;
using SalesManagementSystem.Services;

namespace SalesManagementSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CustomerController : ControllerBase
{

    private readonly CustomerService _customerService;

    public CustomerController(CustomerService customerService)
    {
        _customerService = customerService;
    }


    [HttpGet]
    public async Task<IActionResult> GetAllCustomers()
    {
        var customers = await _customerService.GetAllCustomersAsync();

        return Ok(customers);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomer(int id)
    {
        var customer = await _customerService
            .GetCustomerByIdAsync(id);

        if (customer == null)
        {
            return NotFound("Customer not found");
        }

        return Ok(customer);
    }


    [HttpPost]
    public async Task<IActionResult> CreateCustomer(CustomerDto model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var codeExists = await _customerService
            .CustomerCodeExistsAsync(model.Code);

        if (codeExists)
        {
            return Conflict("Customer code already exists");
        }

        var customer = await _customerService
            .CreateCustomerAsync(model);

        return CreatedAtAction(
            nameof(GetCustomer),
            new { id = customer.Id },
            customer
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomer(int id,CustomerDto model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var customer = await _customerService
            .GetCustomerByIdAsync(id);

        if (customer == null)
        {
            return NotFound("Customer not found");
        }

        var codeExists = await _customerService
            .CustomerCodeExistsAsync(model.Code, id);

        if (codeExists)
        {
            return Conflict("Customer code already exists");
        }

        var updatedCustomer = await _customerService
            .UpdateCustomerAsync(id, model);

        return Ok(updatedCustomer);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        var result = await _customerService
            .DeleteCustomerAsync(id);

        if (!result)
        {
            return NotFound("Customer not found");
        }

        return Ok("Customer deleted successfully");
    }
}
