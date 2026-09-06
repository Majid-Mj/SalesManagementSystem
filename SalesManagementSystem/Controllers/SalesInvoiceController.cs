using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesManagementSystem.DTOs;
using SalesManagementSystem.Models;
using SalesManagementSystem.Services;

namespace SalesManagementSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SalesInvoiceController : ControllerBase
    {
        private readonly SalesInvoiceService _invoiceService;

        public SalesInvoiceController(SalesInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

    [HttpPost]
    public async Task<IActionResult> AddInvoice(AddInvoiceDto dto)
    {
        var invoice = new SalesInvoiceMaster
        {
            InvoiceNumber = dto.InvoiceNumber,
            InvoiceDate = dto.InvoiceDate,
            CustomerId = dto.CustomerId,
            Details = dto.Details?.Select(d => new SalesInvoiceDetail
            {
                ProductId = d.ProductId,
                Quantity = d.Quantity
            }).ToList() ?? new List<SalesInvoiceDetail>()
        };

        var result = await _invoiceService.AddAsync(invoice);
        if (result.Error != null)
        {
            return BadRequest(new { message = result.Error });
        }
        return Ok(result.Data);
    }


    [HttpGet]
    public async Task<IActionResult> GetInvoices()
    {
        return Ok(await _invoiceService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetInvoiceById(int id)
    {
        var invoice = await _invoiceService.GetByIdAsync(id);
        if (invoice == null)
        {
            return NotFound(new { message = "Invoice not found" });
        }
        return Ok(invoice);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInvoice(int id)
    {
        var success = await _invoiceService.DeleteAsync(id);
        if (!success)
        {
            return NotFound(new { message = "Invoice not found" });
        }
        return Ok(new { message = "Invoice deleted successfully" });
    }
}
