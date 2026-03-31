using TB5.MinimalApi2.Features.Product;
using TB5.MinimalApi2.Features.ProductCategory;
using TB5.Shared;
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

//app.MapProductEndpoints();
app.MapProductCategoryEndpoints();

app.Run();

string a = "1";
decimal b = a.ToDecimal();
