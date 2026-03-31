namespace TB5.Domain.Features.ProductCategory.Models;

public class ProductCategoryCreateRequest
{
    public string ProductCategoryName { get; set; } = null!;
}

public class ProductCategoryUpdateRequest
{
    public string ProductCategoryName { get; set; } = null!;
}

public class ProductCategoryResponse
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = null!;
}
