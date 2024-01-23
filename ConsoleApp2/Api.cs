using System.Text.Json;

namespace ConsoleApp2;
public static class Api
{
    static readonly HttpClient _httpClient = new HttpClient();
    static async Task<ApiObject> GetFromApi(int id)
    {
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync($"https://jsonplaceholder.typicode.com/posts/{id}");
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ApiObject>(jsonString);
        }
    }
}
