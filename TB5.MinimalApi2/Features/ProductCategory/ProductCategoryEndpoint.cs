using TB5.MinimalApi2.Features.ProductCategory.Models;
using TB5.WebApi.Database.AppDbContextModels;

namespace TB5.MinimalApi2.Features.ProductCategory;

public static class ProductCategoryEndpoint
{
    public static void MapProductCategoryEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/product-category", () =>
        {
            AppDbContext db = new AppDbContext();
            List<TblProductCategory> lst = db.TblProductCategories.ToList();
            return Results.Ok(lst);
        })
        .WithName("GetProductCategories")
        .WithOpenApi();

        app.MapGet("/api/product-category/{id}", (int id) =>
        {
            AppDbContext db = new AppDbContext();
            TblProductCategory? category = db.TblProductCategories.FirstOrDefault(x => x.ProductCategoryId == id);
            if (category == null)
            {
                return Results.NotFound(new { Message = "Product category not found." });
            }
            return Results.Ok(category);
        })
        .WithName("GetProductCategoryById")
        .WithOpenApi();

        app.MapPost("/api/product-category", (ProductCategoryCreateRequest request) =>
        {
            AppDbContext db = new AppDbContext();
            TblProductCategory category = new TblProductCategory
            {
                ProductCategoryName = request.ProductCategoryName
            };
            db.TblProductCategories.Add(category);
            int result = db.SaveChanges();

            bool isSuccess = result > 0;
            ProductCategoryResponse response = new ProductCategoryResponse
            {
                IsSuccess = isSuccess,
                Message = isSuccess ? "Product category created successfully." : "Failed to create product category.",
                Id = category.ProductCategoryId
            };
            return isSuccess ? Results.Ok(response) : Results.BadRequest(response);
        })
        .WithName("CreateProductCategory")
        .WithOpenApi();

        app.MapPut("/api/product-category/{id}", (int id, ProductCategoryUpdateRequest request) =>
        {
            AppDbContext db = new AppDbContext();
            TblProductCategory? category = db.TblProductCategories.FirstOrDefault(x => x.ProductCategoryId == id);
            if (category == null)
            {
                return Results.NotFound(new { Message = "Product category not found." });
            }

            category.ProductCategoryName = request.ProductCategoryName;

            db.TblProductCategories.Update(category);
            int result = db.SaveChanges();

            bool isSuccess = result > 0;
            return isSuccess ? Results.Ok(new { IsSuccess = true, Message = "Product category updated successfully." }) : Results.BadRequest(new { IsSuccess = false, Message = "Failed to update product category." });
        })
        .WithName("UpdateProductCategory")
        .WithOpenApi();

        app.MapDelete("/api/product-category/{id}", (int id) =>
        {
            AppDbContext db = new AppDbContext();
            TblProductCategory? category = db.TblProductCategories.FirstOrDefault(x => x.ProductCategoryId == id);
            if (category == null)
            {
                return Results.NotFound(new { Message = "Product category not found." });
            }

            db.TblProductCategories.Remove(category);
            int result = db.SaveChanges();

            bool isSuccess = result > 0;
            return isSuccess ? Results.Ok(new { IsSuccess = true, Message = "Product category deleted successfully." }) : Results.BadRequest(new { IsSuccess = false, Message = "Failed to delete product category." });
        })
        .WithName("DeleteProductCategory")
        .WithOpenApi();
    }
}
