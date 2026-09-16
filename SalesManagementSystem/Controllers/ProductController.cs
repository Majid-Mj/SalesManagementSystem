using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesManagementSystem.Application.DTOs;
using SalesManagementSystem.Application.Interfaces;

namespace SalesManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] ProductDto product)
    {
        var response = await _productService.AddAsync(product);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var response = await _productService.GetAllAsync();
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var response = await _productService.GetByIdAsync(id);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDto product)
    {
        var response = await _productService.UpdateAsync(id, product);
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var response = await _productService.DeleteAsync(id);
        return StatusCode(response.StatusCode, response);
    }
}
