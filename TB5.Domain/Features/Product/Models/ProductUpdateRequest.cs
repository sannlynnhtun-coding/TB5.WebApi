namespace TB5.Domain.Features.Product.Models;

public class ProductUpdateRequest
{
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
}
