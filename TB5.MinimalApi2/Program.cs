using TB5.MinimalApi2.Models;
using TB5.WebApi.Database.AppDbContextModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/api/product", () =>
{
    AppDbContext db = new AppDbContext();
    List<TblProduct> lst = db.TblProducts.Where(x => !x.IsDelete).ToList();
    return Results.Ok(lst);
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

app.Run();
