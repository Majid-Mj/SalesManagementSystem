using System.ComponentModel.DataAnnotations;

namespace SalesManagementSystem.DTOs;

public class CustomerDto
{
    [Required(ErrorMessage = "Customer name is required")]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Customer code is required")]
    [MaxLength(50)]
    public string Code { get; set; } = string.Empty;

    [MaxLength(255)]
    public string? Address { get; set; }
}