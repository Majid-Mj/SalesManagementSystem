using Microsoft.EntityFrameworkCore;
using SalesManagementSystem.Data;
using SalesManagementSystem.Models;
using System.Text.Json;

namespace SalesManagementSystem.Services;

public class SalesInvoiceService
{

    private readonly AppDbContext _context;

    public SalesInvoiceService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<SalesInvoiceMaster>> GetAllAsync()
    {
        return await _context.SalesInvoiceMaster
            .FromSqlRaw("SELECT * FROM sp_get_all_invoices()")
            .Include(i => i.Customer)
            .Include(i => i.Details)
                .ThenInclude(d => d.Product)
            .ToListAsync();
    }

    public async Task<SalesInvoiceMaster?> GetByIdAsync(int id)
    {
        return await _context.SalesInvoiceMaster
            .FromSqlInterpolated($"SELECT * FROM sp_get_invoice_by_id({id})")
            .Include(i => i.Customer)
            .Include(i => i.Details)
                .ThenInclude(d => d.Product)
            .FirstOrDefaultAsync();
    }

    public async Task<(string? Error, SalesInvoiceMaster? Data)> AddAsync(SalesInvoiceMaster invoice)
    {
        var customerExists = await _context.Customers.AnyAsync(c => c.Id == invoice.CustomerId);
        if (!customerExists) return ("Customer not found", null);

        if (invoice.Details == null || !invoice.Details.Any())
            return ("Invoice must have at least one product", null);

        decimal total = 0;

        foreach (var detail in invoice.Details)
        {
            var product = await _context.Products.FindAsync(detail.ProductId);
            if (product == null)
                return ($"Product with Id {detail.ProductId} not found", null);

            detail.Rate = product.Price;             
            detail.Amount = detail.Quantity * detail.Rate;
            total += detail.Amount;
        }

        invoice.TotalAmount = total;

        _context.SalesInvoiceMaster.Add(invoice);
        await _context.SaveChangesAsync();

        return (null, invoice);
    }


    public async Task<bool> DeleteAsync(int id)
    {
        var invoice = await _context.SalesInvoiceMaster
            .Include(i => i.Details)
            .FirstOrDefaultAsync(i => i.Id == id);

        if (invoice == null) return false;

        _context.SalesInvoiceDetails.RemoveRange(invoice.Details);
        _context.SalesInvoiceMaster.Remove(invoice);
        await _context.SaveChangesAsync();
        return true;
    }
}
