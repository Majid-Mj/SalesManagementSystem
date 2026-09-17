using Microsoft.EntityFrameworkCore;
using SalesManagementSystem.Domain.Entities;

namespace SalesManagementSystem.Application.Interfaces;

public interface IAppDbContext
{
    DbSet<User> Users { get; set; }
    DbSet<Product> Products { get; set; }
    DbSet<Customer> Customers { get; set; }
    DbSet<SalesInvoiceMaster> SalesInvoiceMaster { get; set; }
    DbSet<SalesInvoiceDetail> SalesInvoiceDetails { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
