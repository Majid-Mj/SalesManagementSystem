using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SalesManagementSystem.Domain.Entities;

public class Customer
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string Code { get; set; } = string.Empty;

    [MaxLength(200)]
    public string Address { get; set; } = string.Empty;

    [JsonIgnore]
    public ICollection<SalesInvoiceMaster> Invoices { get; set; } = new List<SalesInvoiceMaster>();
}
