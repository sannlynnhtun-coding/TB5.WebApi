using TB5.Domain.Features.Product;
using TB5.MinimalApi2.Features.Product.Models;
using TB5.WebApi.Database.AppDbContextModels;

namespace TB5.MinimalApi2.Features.Product;

public static class ProductEndpoint
{
    public static void MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/product", () =>
        {
            ProductService service = new ProductService();
            return Results.Ok(service.GetProducts());
        })
        .WithName("GetProducts")
        .WithOpenApi();

        app.MapGet("/api/product/{id}", (int id) =>
        {
            AppDbContext db = new AppDbContext();
            TblProduct? product = db.TblProducts.FirstOrDefault(x => x.Id == id && !x.IsDelete);
            if (product == null)
            {
                return Results.NotFound(new { Message = "Product not found." });
            }
            return Results.Ok(product);
        })
        .WithName("GetProductById")
        .WithOpenApi();

        app.MapPost("/api/product", (ProductCreateRequest request) =>
        {
            AppDbContext db = new AppDbContext();
            TblProduct product = new TblProduct
            {
                CreatedDateTime = DateTime.Now,
                IsDelete = false,
                Name = request.Name,
                Price = request.Price,
            };
            db.TblProducts.Add(product);
            int result = db.SaveChanges();

            bool isSuccess = result > 0;
            ProductCreateResponse response = new ProductCreateResponse
            {
                IsSuccess = isSuccess,
                Message = isSuccess ? "Product created successfully." : "Failed to create product.",
                Id = product.Id
            };
            return isSuccess ? Results.Ok(response) : Results.BadRequest(response);
        })
        .WithName("CreateProduct")
        .WithOpenApi();

        app.MapPut("/api/product/{id}", (int id, ProductUpdateRequest request) =>
        {
            AppDbContext db = new AppDbContext();
            TblProduct? product = db.TblProducts.FirstOrDefault(x => x.Id == id && !x.IsDelete);
            if (product == null)
            {
                return Results.NotFound(new { Message = "Product not found." });
            }

            product.Name = request.Name;
            product.Price = request.Price;
            product.ModifiedDateTime = DateTime.Now;

            db.TblProducts.Update(product);
            int result = db.SaveChanges();

            bool isSuccess = result > 0;
            return isSuccess ? Results.Ok(new { IsSuccess = true, Message = "Product updated successfully." }) : Results.BadRequest(new { IsSuccess = false, Message = "Failed to update product." });
        })
        .WithName("UpdateProduct")
        .WithOpenApi();

        app.MapDelete("/api/product/{id}", (int id) =>
        {
            AppDbContext db = new AppDbContext();
            TblProduct? product = db.TblProducts.FirstOrDefault(x => x.Id == id && !x.IsDelete);
            if (product == null)
            {
                return Results.NotFound(new { Message = "Product not found." });
            }

            product.IsDelete = true;
            product.ModifiedDateTime = DateTime.Now;

            db.TblProducts.Update(product);
            int result = db.SaveChanges();

            bool isSuccess = result > 0;
            return isSuccess ? Results.Ok(new { IsSuccess = true, Message = "Product deleted successfully." }) : Results.BadRequest(new { IsSuccess = false, Message = "Failed to delete product." });
        })
        .WithName("DeleteProduct")
        .WithOpenApi();
    }
}
