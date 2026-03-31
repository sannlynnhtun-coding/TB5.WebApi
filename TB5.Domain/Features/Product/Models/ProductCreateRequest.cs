namespace TB5.Domain.Features.Product.Models;

public class ProductCreateRequest
{
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
}
