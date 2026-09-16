using System.ComponentModel.DataAnnotations;

namespace SalesManagementSystem.Application.DTOs;

public class CustomerDto
{
    [Required(ErrorMessage = "Customer name is required")]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Customer code is required")]
    [MaxLength(20)]
    public string Code { get; set; } = string.Empty;

    [MaxLength(200)]
    public string Address { get; set; } = string.Empty;
}
