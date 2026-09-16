using System.ComponentModel.DataAnnotations;

namespace SalesManagementSystem.Application.DTOs;

public class AddInvoiceDto
{
    [Required]
    [MaxLength(50)]
    public string InvoiceNumber { get; set; } = string.Empty;

    [Required]
    public DateTime InvoiceDate { get; set; }

    [Required]
    public int CustomerId { get; set; }

    [Required]
    [MinLength(1, ErrorMessage = "Invoice must have at least one line item")]
    public List<InvoiceDetailDto> Details { get; set; } = new();
}

public class InvoiceDetailDto
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "ProductId must be valid")]
    public int ProductId { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
    public int Quantity { get; set; }
}
