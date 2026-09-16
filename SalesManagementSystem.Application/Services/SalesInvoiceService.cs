using Microsoft.EntityFrameworkCore;
using SalesManagementSystem.Application.Common;
using SalesManagementSystem.Infrastructure.Data;
using SalesManagementSystem.Application.DTOs;
using SalesManagementSystem.Application.Interfaces;
using SalesManagementSystem.Domain.Entities;

namespace SalesManagementSystem.Application.Services;

public class SalesInvoiceService : ISalesInvoiceService
{
    private readonly AppDbContext _context;

    public SalesInvoiceService(AppDbContext context) => _context = context;

    public async Task<ApiResponse<List<SalesInvoiceMaster>>> GetAllAsync()
    {
        var invoices = await _context.SalesInvoiceMaster
            .FromSqlRaw("SELECT * FROM sp_get_all_invoices()")
            .AsNoTracking()
            .Include(i => i.Customer)
            .Include(i => i.Details)
                .ThenInclude(d => d.Product)
            .ToListAsync();

        return ApiResponse<List<SalesInvoiceMaster>>.SuccessResponse(invoices);
    }

    public async Task<ApiResponse<SalesInvoiceMaster>> GetByIdAsync(int id)
    {
        var invoice = await _context.SalesInvoiceMaster
            .FromSqlInterpolated($"SELECT * FROM sp_get_invoice_by_id({id})")
            .AsNoTracking()
            .Include(i => i.Customer)
            .Include(i => i.Details)
                .ThenInclude(d => d.Product)
            .FirstOrDefaultAsync();

        return invoice is null
            ? ApiResponse<SalesInvoiceMaster>.NotFound("Invoice not found")
            : ApiResponse<SalesInvoiceMaster>.SuccessResponse(invoice);
    }

    public async Task<ApiResponse<SalesInvoiceMaster>> AddAsync(AddInvoiceDto dto)
    {
        if (dto.Details is null or { Count: 0 })
            return ApiResponse<SalesInvoiceMaster>.BadRequest("Invoice must have at least one product line item");

        if (await _context.SalesInvoiceMaster.AnyAsync(i => i.InvoiceNumber == dto.InvoiceNumber))
            return ApiResponse<SalesInvoiceMaster>.Conflict($"Invoice number '{dto.InvoiceNumber}' already exists.");

        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == dto.CustomerId);
        if (customer is null)
            return ApiResponse<SalesInvoiceMaster>.NotFound($"Customer with Id {dto.CustomerId} not found");

        var productIds = dto.Details.Select(d => d.ProductId).ToHashSet();
        if (productIds.Count != dto.Details.Count)
            return ApiResponse<SalesInvoiceMaster>.BadRequest("Invoice payload contains duplicate product line items");

        var products = await _context.Products
            .Where(p => productIds.Contains(p.Id))
            .ToDictionaryAsync(p => p.Id);

        if (products.Count != productIds.Count)
        {
            var missingId = productIds.First(id => !products.ContainsKey(id));
            return ApiResponse<SalesInvoiceMaster>.NotFound($"Product with Id {missingId} not found");
        }

        var invoiceDetails = dto.Details.Select(detail =>
        {
            var product = products[detail.ProductId];
            var rate = product.Price;
            return new SalesInvoiceDetail
            {
                ProductId = detail.ProductId,
                Product = product,
                Quantity = detail.Quantity,
                Rate = rate,
                Amount = detail.Quantity * rate
            };
        }).ToList();

        var invoice = new SalesInvoiceMaster
        {
            InvoiceNumber = dto.InvoiceNumber,
            InvoiceDate = dto.InvoiceDate,
            CustomerId = dto.CustomerId,
            Customer = customer,
            TotalAmount = invoiceDetails.Sum(d => d.Amount),
            Details = invoiceDetails
        };

        _context.SalesInvoiceMaster.Add(invoice);
        await _context.SaveChangesAsync();

        return ApiResponse<SalesInvoiceMaster>.Created(invoice, "Invoice created successfully");
    }

    public async Task<ApiResponse> DeleteAsync(int id)
    {
        var invoice = await _context.SalesInvoiceMaster.FindAsync(id);
        if (invoice is null)
            return ApiResponse.NotFound("Invoice not found");

        _context.SalesInvoiceMaster.Remove(invoice);
        await _context.SaveChangesAsync();

        return ApiResponse.SuccessResponse("Invoice deleted successfully");
    }
}
