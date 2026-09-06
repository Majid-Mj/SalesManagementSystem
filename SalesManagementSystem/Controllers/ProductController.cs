using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesManagementSystem.Models;
using SalesManagementSystem.Services;

namespace SalesManagementSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProductController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductController(ProductService productService)
    {
        _productService = productService;
    }



    //Addproduct
    [HttpPost]
    public async Task<IActionResult> AddProduct(Product product)
    {
        var error = await _productService.AddAsync(product);
        if (error != null)
        {
            return BadRequest(new { message = error });
        }
        return Ok(product);
    }

    //Get product
    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _productService.GetAllAsync();
        return Ok(products);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product == null)
        {
            return NotFound(new { message = "Product not found" });
        }

        return Ok(product);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, Product product)
    {
        var error = await _productService.UpdateAsync(id, product);
        if (error != null)
        {
            return BadRequest(new { message = error });
        }
        return Ok(new { message = "Product updated successfully" });
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var success = await _productService.DeleteAsync(id);
        if (!success)
        {
            return NotFound(new { message = "Product not found" });
        }
            
        return Ok(new { message = "Product deleted successfully" });
    }
}
