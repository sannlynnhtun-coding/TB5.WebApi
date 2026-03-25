namespace TB5.WebApi.Models
{
    public class ProductCreateRequest
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
