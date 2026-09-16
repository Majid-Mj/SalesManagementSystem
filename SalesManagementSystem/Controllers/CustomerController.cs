using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesManagementSystem.Application.DTOs;
using SalesManagementSystem.Application.Interfaces;

namespace SalesManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCustomers()
    {
        var response = await _customerService.GetAllCustomersAsync();
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomer(int id)
    {
        var response = await _customerService.GetCustomerByIdAsync(id);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] CustomerDto model)
    {
        var response = await _customerService.CreateCustomerAsync(model);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerDto model)
    {
        var response = await _customerService.UpdateCustomerAsync(id, model);
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        var response = await _customerService.DeleteCustomerAsync(id);
        return StatusCode(response.StatusCode, response);
    }
}
