namespace TB5.MinimalApi2.Features.Product.Models;

public class ProductUpdateRequest
{
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
}
