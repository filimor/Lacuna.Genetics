using System.Net.Http.Headers;
using System.Net.Http.Json;
using Lacuna.Genetics.Core.Exceptions;
using Lacuna.Genetics.Core.Interfaces;
using Lacuna.Genetics.Core.Models;

namespace Lacuna.Genetics.Core;

public class HttpService:IHttpService
{
    private static readonly HttpClient Client = new()
    {
        BaseAddress = new Uri("https://gene.lacuna.cc/")
    };


    public async Task<string?> RequestAccessTokenAsync(User user)
    {
        var client = GetHttpClient();
        var response = await client.PostAsJsonAsync("api/users/login", user);
        return GetResponseContent(response).Result.AccessToken;
    }

    public async Task<Job?> RequestJobAsync(string accessToken)
    {
        var client = GetHttpClient(accessToken);
        var response = await client.GetAsync("api/dna/jobs");
        return GetResponseContent(response).Result.Job;
    }

    public async Task<Response> SubmitEncodeStrandAsync(string accessToken, string jobId, Result result)
    {
        var client = GetHttpClient(accessToken);
        var response = await client.PostAsJsonAsync($"api/dna/jobs/{jobId}/encode", result);
        return await GetResponseContent(response);
    }

    public async Task<Response> SubmitDecodeStrandAsync(string accessToken, string jobId, Result result)
    {
        var client = GetHttpClient(accessToken);
        var response = await client.PostAsJsonAsync($"api/dna/jobs/{jobId}/decode", result);
        return await GetResponseContent(response);
    }

    public async Task<Response> SubmitCheckGeneAsync(string accessToken, string jobId, Result result)
    {
        var client = GetHttpClient(accessToken);
        var response = await client.PostAsJsonAsync($"api/dna/jobs/{jobId}/gene", result);
        return await GetResponseContent(response);
    }

    private static HttpClient GetHttpClient(string accessToken = "")
    {
        Client.DefaultRequestHeaders.Accept.Clear();
        Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        if (accessToken != "")
        {
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        return Client;
    }

    private static async Task<Response> GetResponseContent(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpException(
                $"Failed to send the request.\nStatus code: {response.StatusCode}\nResponse: {response.Content.ReadAsStringAsync().Result}\n");
        }

        var responseContent = await response.Content.ReadFromJsonAsync<Response>();
        
        return responseContent.Code == "Success"
            ? responseContent
            : throw new ApiException(
                $"Failed to get/submit data.\nCode: {responseContent.Code}\nMessage: {responseContent.Message}");
    }
}