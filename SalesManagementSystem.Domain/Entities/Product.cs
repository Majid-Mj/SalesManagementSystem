using System.ComponentModel.DataAnnotations;

namespace SalesManagementSystem.Domain.Entities;

public class Product
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string Code { get; set; } = string.Empty;

    public decimal Cost { get; set; }
    public decimal Price { get; set; }
}
