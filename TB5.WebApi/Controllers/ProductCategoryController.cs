using Microsoft.AspNetCore.Mvc;
using TB5.Domain.Features.ProductCategory;
using TB5.Domain.Features.ProductCategory.Models;
using TB5.WebApi.Database.AppDbContextModels;

namespace TB5.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductCategoryController : ControllerBase
{
    private readonly ProductCategoryService _productCategoryService;

    public ProductCategoryController()
    {
        _productCategoryService = new ProductCategoryService();
    }

    [HttpGet]
    public IActionResult GetProductCategories()
    {
        return Ok(_productCategoryService.GetProductCategories());
    }

    [HttpGet("{id}")]
    public IActionResult GetProductCategory(int id)
    {
        var category = _productCategoryService.GetProductCategory(id);
        if (category == null) return NotFound(new { Message = "Product Category not found." });
        return Ok(category);
    }

    [HttpPost]
    public IActionResult CreateProductCategory(ProductCategoryCreateRequest request)
    {
        var response = _productCategoryService.CreateProductCategory(request);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateProductCategory(int id, ProductCategoryUpdateRequest request)
    {
        var response = _productCategoryService.UpdateProductCategory(id, request);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProductCategory(int id)
    {
        var response = _productCategoryService.DeleteProductCategory(id);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }
}
