using System.ComponentModel.DataAnnotations;

namespace SalesManagementSystem.Models;


public class Product
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Product name is required")]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Product code is required")]
    [MaxLength(50)]
    public string Code { get; set; } = string.Empty;

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Cost must be greater than 0")]
    public decimal Cost { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public decimal Price { get; set; }
}