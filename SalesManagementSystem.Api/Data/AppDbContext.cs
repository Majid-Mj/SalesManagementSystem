using SalesManagementSystem.Models;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace SalesManagementSystem.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<SalesInvoiceMaster> SalesInvoiceMaster { get; set; }
    public DbSet<SalesInvoiceDetail> SalesInvoiceDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasIndex(p => p.Code).IsUnique();
        modelBuilder.Entity<Customer>().HasIndex(c => c.Code).IsUnique();

        modelBuilder.Entity<SalesInvoiceDetail>()
    .HasOne(d => d.Invoice)
    .WithMany(i => i.Details)
    .HasForeignKey(d => d.InvoiceId);

        modelBuilder.Entity<SalesInvoiceDetail>()
            .HasOne(d => d.Product)
            .WithMany()
            .HasForeignKey(d => d.ProductId);

        modelBuilder.Entity<SalesInvoiceMaster>()
            .HasOne(i => i.Customer)
            .WithMany(c => c.Invoices)
            .HasForeignKey(i => i.CustomerId);
    }


}