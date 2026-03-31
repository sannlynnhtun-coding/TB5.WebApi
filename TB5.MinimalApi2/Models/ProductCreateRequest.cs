namespace TB5.MinimalApi2.Models;

public class ProductCreateRequest
{
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
}
