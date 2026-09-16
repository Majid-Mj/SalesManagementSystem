using System.ComponentModel.DataAnnotations;

namespace SalesManagementSystem.Application.DTOs;

public class ProductDto
{
    [Required(ErrorMessage = "Product name is required")]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Product code is required")]
    [MaxLength(20)]
    public string Code { get; set; } = string.Empty;

    [Range(0, double.MaxValue, ErrorMessage = "Cost must be non-negative")]
    public decimal Cost { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Price must be non-negative")]
    public decimal Price { get; set; }
}
