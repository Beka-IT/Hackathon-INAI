using System.Net.Http.Headers;
using System.Text.Json.Serialization;

namespace WebApi.Controllers;

class ChatGptResponse
{
    [JsonPropertyName("choices")]
    public List<Choice> Choices { get; set; }
}

class Choice
{
    [JsonPropertyName("text")]
    public String Text { get; set; }
}

public class OpenAIApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    

    public OpenAIApiClient(string apiKey = "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")
        // "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" 
        // "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"
    {
        _apiKey = apiKey;
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("https://api.openai.com/v1/");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
    }

    public async Task<string> SendPrompt(string prompt, string model = "text-davinci-003")
    {
        var requestBody = new
        {
            prompt = prompt,
            model = model,
            max_tokens = 150,
            temperature = 0.5
        };

        var response = await _httpClient.PostAsJsonAsync("completions", requestBody);
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();

        return responseBody;
    }
}