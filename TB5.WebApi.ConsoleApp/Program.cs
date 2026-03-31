// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using TB5.Shared;
using TB5.Domain.Features.Product;
using TB5.Domain.Features.Product.Models;
using TB5.Domain.Features.ProductCategory;
using TB5.Domain.Features.ProductCategory.Models;

Console.WriteLine("TB5 WebApi Console App - Domain Service Demo");

// Product Demo
ProductService productService = new ProductService();
Console.WriteLine("\n--- Product Demo ---");
var products = productService.GetProducts();
Console.WriteLine($"Total Products: {products.Count}");

// Create Product
var createRequest = new ProductCreateRequest { Name = "Console Test Product", Price = 99.99m };
var createResponse = productService.CreateProduct(createRequest);
Console.WriteLine($"Create Product Success: {createResponse.IsSuccess}, Id: {createResponse.Id}");

// ProductCategory Demo
ProductCategoryService categoryService = new ProductCategoryService();
Console.WriteLine("\n--- Product Category Demo ---");
var categories = categoryService.GetProductCategories();
Console.WriteLine($"Total Categories: {categories.Count}");

// Create Category
var categoryRequest = new ProductCategoryCreateRequest { ProductCategoryName = "Console Test Category" };
var categoryResponse = categoryService.CreateProductCategory(categoryRequest);
Console.WriteLine($"Create Category Success: {categoryResponse.IsSuccess}, Message: {categoryResponse.Message}");

Console.WriteLine("\nPress Enter to exit...");
Console.ReadLine();
