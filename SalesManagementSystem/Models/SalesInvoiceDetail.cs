using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SalesManagementSystem.Models;

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

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
    public int Quantity { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Rate must be greater than 0")]
    public decimal Rate { get; set; }

    public decimal Amount { get; set; }
}