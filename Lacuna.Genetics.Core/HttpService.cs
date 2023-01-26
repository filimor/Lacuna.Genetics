using System.Net.Http.Headers;
using System.Net.Http.Json;
using Lacuna.Genetics.Core.Model;

namespace Lacuna.Genetics.Core;

// TODO: Encapsulate the classes better
public class HttpService
{
    public async Task<string?> RequestAccessTokenAsync(User user)
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri("https://gene.lacuna.cc/");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


        // TODO: Implement error handling
        var response = await client.PostAsJsonAsync("api/users/login", user);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"{response.StatusCode} - {response.Content}");
        }

        var responseContent = await response.Content.ReadFromJsonAsync<Response>();
        return responseContent?.AccessToken;
    }

    public async Task<Job?> RequestJobAsync(string accessToken)
    {
        // TODO: Remove duplicated code
        using var client = new HttpClient();
        client.BaseAddress = new Uri("https://gene.lacuna.cc/");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var response = await client.GetAsync("api/dna/jobs");
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"{response.StatusCode} - {response.Content}");
        }

        var responseContent = await response.Content.ReadFromJsonAsync<Response>();
        return responseContent?.Job;
    }

    // TODO: Change method signatures?
    public async Task<Response> SubmitEncodeStrandAsync(string accessToken, string jobId, Result result)
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri("https://gene.lacuna.cc/");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var response = await client.PostAsJsonAsync($"api/dna/jobs/{jobId}/encode", result);
        var responseContent = await response.Content.ReadFromJsonAsync<Response>();
        return responseContent;
    }

    public async Task<Response> SubmitDecodeStrandAsync(string accessToken, string jobId, Result result)
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri("https://gene.lacuna.cc/");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var response = await client.PostAsJsonAsync($"api/dna/jobs/{jobId}/decode", result);
        var responseContent = await response.Content.ReadFromJsonAsync<Response>();
        // TODO: Implement error handling on the proper level
        return responseContent;
    }

    public async Task<Response> SubmitCheckGeneAsync(string accessToken, string jobId, Result result)
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri("https://gene.lacuna.cc/");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var response = await client.PostAsJsonAsync($"api/dna/jobs/{jobId}/check", result);
        var responseContent = await response.Content.ReadFromJsonAsync<Response>();
        return responseContent;
    }
}