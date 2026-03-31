using Microsoft.AspNetCore.Mvc;
using TB5.Domain.Features.Product;
using TB5.Domain.Features.Product.Models;
using TB5.WebApi.Database.AppDbContextModels;

namespace TB5.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductController()
    {
        _productService = new ProductService();
    }

    [HttpGet]
    public IActionResult GetProducts()
    {
        return Ok(_productService.GetProducts());
    }

    [HttpGet("{id}")]
    public IActionResult GetProduct(int id)
    {
        var product = _productService.GetProduct(id);
        if (product == null) return NotFound(new { Message = "Product not found." });
        return Ok(product);
    }

    [HttpPost]
    public IActionResult CreateProduct(ProductCreateRequest request)
    {
        var response = _productService.CreateProduct(request);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateProduct(int id, ProductUpdateRequest request)
    {
        bool isSuccess = _productService.UpdateProduct(id, request);
        return isSuccess ? Ok(new { IsSuccess = true, Message = "Product updated successfully." }) : BadRequest(new { IsSuccess = false, Message = "Failed to update product." });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        bool isSuccess = _productService.DeleteProduct(id);
        return isSuccess ? Ok(new { IsSuccess = true, Message = "Product deleted successfully." }) : BadRequest(new { IsSuccess = false, Message = "Failed to delete product." });
    }
}