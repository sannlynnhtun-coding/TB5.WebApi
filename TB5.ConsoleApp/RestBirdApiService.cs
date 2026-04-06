using RestSharp;

namespace TB5.ConsoleApp;

public class RestBirdApiService
{
    private readonly RestClient _client;
    private readonly string _baseUrl = "https://fake-brids-apis.vercel.app/api/v1/birds";

    public RestBirdApiService()
    {
        _client = new RestClient(_baseUrl);
    }

    public async Task<List<BirdModel>> Read()
    {
        var request = new RestRequest("");
        var response = await _client.ExecuteGetAsync<List<BirdModel>>(request);
        return response.Data ?? new List<BirdModel>();
    }

    public async Task<BirdModel?> Read(int id)
    {
        var request = new RestRequest($"{id}");
        var response = await _client.ExecuteGetAsync<BirdModel>(request);
        return response.Data;
    }

    public async Task Create(BirdModel bird)
    {
        var request = new RestRequest("");
        request.AddJsonBody(bird);
        var response = await _client.ExecutePostAsync(request);
        if (response.IsSuccessful)
        {
            Console.WriteLine("Bird created successfully: " + response.Content);
        }
        else
        {
            Console.WriteLine($"Bird creation failed: {response.StatusCode}");
        }
    }

    public async Task Update(int id, BirdModel bird)
    {
        var request = new RestRequest($"{id}");
        request.AddJsonBody(bird);
        var response = await _client.ExecutePutAsync(request);
        if (response.IsSuccessful)
        {
            Console.WriteLine("Bird updated successfully: " + response.Content);
        }
        else
        {
            Console.WriteLine($"Bird update failed: {response.StatusCode}");
        }
    }

    public async Task Patch(int id, BirdModel bird)
    {
        var request = new RestRequest($"{id}", Method.Patch);
        request.AddJsonBody(bird);
        var response = await _client.ExecuteAsync(request);
        if (response.IsSuccessful)
        {
            Console.WriteLine("Bird patched successfully: " + response.Content);
        }
        else
        {
            Console.WriteLine($"Bird patch failed: {response.StatusCode}");
        }
    }

    public async Task Delete(int id)
    {
        var request = new RestRequest($"{id}", Method.Delete);
        var response = await _client.ExecuteAsync(request);
        if (response.IsSuccessful)
        {
            Console.WriteLine("Bird deleted successfully: " + response.Content);
        }
        else
        {
            Console.WriteLine($"Bird deletion failed: {response.StatusCode}");
        }
    }
}
