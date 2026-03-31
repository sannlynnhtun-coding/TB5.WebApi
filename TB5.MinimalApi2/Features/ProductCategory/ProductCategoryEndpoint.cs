using TB5.Domain.Features.ProductCategory;
using TB5.Domain.Features.ProductCategory.Models;
using TB5.WebApi.Database.AppDbContextModels;

namespace TB5.MinimalApi2.Features.ProductCategory;

public static class ProductCategoryEndpoint
{
    public static void MapProductCategoryEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/productcategory", () =>
        {
            ProductCategoryService service = new ProductCategoryService();
            return Results.Ok(service.GetProductCategories());
        })
        .WithName("GetProductCategories")
        .WithOpenApi();

        app.MapGet("/api/productcategory/{id}", (int id) =>
        {
            ProductCategoryService service = new ProductCategoryService();
            TblProductCategory? category = service.GetProductCategory(id);
            if (category == null)
            {
                return Results.NotFound(new { Message = "Product Category not found." });
            }
            return Results.Ok(category);
        })
        .WithName("GetProductCategoryById")
        .WithOpenApi();

        app.MapPost("/api/productcategory", (ProductCategoryCreateRequest request) =>
        {
            ProductCategoryService service = new ProductCategoryService();
            var response = service.CreateProductCategory(request);
            return response.IsSuccess ? Results.Ok(response) : Results.BadRequest(response);
        })
        .WithName("CreateProductCategory")
        .WithOpenApi();

        app.MapPut("/api/productcategory/{id}", (int id, ProductCategoryUpdateRequest request) =>
        {
            ProductCategoryService service = new ProductCategoryService();
            var response = service.UpdateProductCategory(id, request);
            return response.IsSuccess ? Results.Ok(response) : Results.BadRequest(response);
        })
        .WithName("UpdateProductCategory")
        .WithOpenApi();

        app.MapDelete("/api/productcategory/{id}", (int id) =>
        {
            ProductCategoryService service = new ProductCategoryService();
            var response = service.DeleteProductCategory(id);
            return response.IsSuccess ? Results.Ok(response) : Results.BadRequest(response);
        })
        .WithName("DeleteProductCategory")
        .WithOpenApi();
    }
}
