using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SalesManagementSystem.Domain.Entities;

public class SalesInvoiceDetail
{
    public int Id { get; set; }

    [Required]
    public int InvoiceId { get; set; }
    [JsonIgnore]
    public SalesInvoiceMaster Invoice { get; set; } = null!;

    [Required]
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public int Quantity { get; set; }
    public decimal Rate { get; set; }
    public decimal Amount { get; set; }
}
