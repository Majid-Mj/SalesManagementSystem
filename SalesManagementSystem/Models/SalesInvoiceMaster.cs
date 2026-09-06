using System.ComponentModel.DataAnnotations;

namespace SalesManagementSystem.Models;

public class SalesInvoiceMaster
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string InvoiceNumber { get; set; } = string.Empty;

    [Required]
    public DateTime InvoiceDate { get; set; }

    [Required]
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;

    public decimal TotalAmount { get; set; }

    public ICollection<SalesInvoiceDetail> Details { get; set; } = new List<SalesInvoiceDetail>();



}