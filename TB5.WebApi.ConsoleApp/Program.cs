// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using TB5.Shared;

Console.WriteLine("Hello, World!");

ProductCreateRequest request = new ProductCreateRequest
{
    Name = "Apple",
    Price = 1000
};
string json = request.ToJson();
Console.WriteLine(json);

// C# object to JSON

Console.ReadLine();

public class ProductCreateRequest
{
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
}
