using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesManagementSystem.Application.DTOs;
using SalesManagementSystem.Application.Interfaces;

namespace SalesManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SalesInvoiceController : ControllerBase
{
    private readonly ISalesInvoiceService _invoiceService;

    public SalesInvoiceController(ISalesInvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }

    [HttpPost]
    public async Task<IActionResult> AddInvoice([FromBody] AddInvoiceDto dto)
    {
        var response = await _invoiceService.AddAsync(dto);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> GetInvoices()
    {
        var response = await _invoiceService.GetAllAsync();
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetInvoiceById(int id)
    {
        var response = await _invoiceService.GetByIdAsync(id);
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInvoice(int id)
    {
        var response = await _invoiceService.DeleteAsync(id);
        return StatusCode(response.StatusCode, response);
    }
}
