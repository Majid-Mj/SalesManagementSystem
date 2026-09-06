using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SalesManagementSystem.Models;

public class Customer
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Customer name is required")]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Customer code is required")]
    [MaxLength(50)]
    public string Code { get; set; } = string.Empty;

    [MaxLength(255)]
    public string? Address { get; set; }

    [JsonIgnore]
    public ICollection<SalesInvoiceMaster> Invoices { get; set; } = new List<SalesInvoiceMaster>();
}