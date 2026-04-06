using Newtonsoft.Json;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace TB5.ConsoleApp;

public class BirdApiService
{
    private readonly HttpClient _client = new HttpClient();
    private readonly string _endpoint = "https://fake-brids-apis.vercel.app/api/v1/birds";

    public async Task<List<BirdModel>> Read()
    {
        var response = await _client.GetAsync(_endpoint);
        if (response.IsSuccessStatusCode)
        {
            string jsonStr = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<BirdModel>>(jsonStr)!;
        }
        return new List<BirdModel>();
    }

    public async Task<BirdModel?> Read(int id)
    {
        var response = await _client.GetAsync($"{_endpoint}/{id}");
        if (response.IsSuccessStatusCode)
        {
            string jsonStr = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<BirdModel>(jsonStr);
        }
        return null;
    }

    public async Task Create(BirdModel bird)
    {
        string jsonStr = JsonConvert.SerializeObject(bird);
        var content = new StringContent(jsonStr, Encoding.UTF8, Application.Json);
        var response = await _client.PostAsync(_endpoint, content);
        if (response.IsSuccessStatusCode)
        {
            string result = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Bird created successfully: " + result);
        }
        else
        {
            Console.WriteLine($"Bird creation failed: {response.StatusCode}");
        }
    }

    public async Task Update(int id, BirdModel bird)
    {
        string jsonStr = JsonConvert.SerializeObject(bird);
        var content = new StringContent(jsonStr, Encoding.UTF8, Application.Json);
        var response = await _client.PutAsync($"{_endpoint}/{id}", content);
        if (response.IsSuccessStatusCode)
        {
            string result = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Bird updated successfully: " + result);
        }
        else
        {
            Console.WriteLine($"Bird update failed: {response.StatusCode}");
        }
    }

    public async Task Patch(int id, BirdModel bird)
    {
        string jsonStr = JsonConvert.SerializeObject(bird);
        var content = new StringContent(jsonStr, Encoding.UTF8, Application.Json);
        var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"{_endpoint}/{id}")
        {
            Content = content
        };
        var response = await _client.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            string result = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Bird patched successfully: " + result);
        }
        else
        {
            Console.WriteLine($"Bird patch failed: {response.StatusCode}");
        }
    }

    public async Task Delete(int id)
    {
        var response = await _client.DeleteAsync($"{_endpoint}/{id}");
        if (response.IsSuccessStatusCode)
        {
            string result = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Bird deleted successfully: " + result);
        }
        else
        {
            Console.WriteLine($"Bird deletion failed: {response.StatusCode}");
        }
    }
}
