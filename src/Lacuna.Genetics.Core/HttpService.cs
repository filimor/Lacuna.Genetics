using System.Net.Http.Headers;
using System.Net.Http.Json;
using Lacuna.Genetics.Core.Exceptions;
using Lacuna.Genetics.Core.Models;

namespace Lacuna.Genetics.Core;

// TODO: Encapsulate the classes better
public static class HttpService
{
    private static readonly HttpClient _client = new()
    {
        BaseAddress = new Uri("https://gene.lacuna.cc/")
    };


    public static async Task<string?> RequestAccessTokenAsync(User user)
    {
        var client = GetHttpClient();
        var response = await client.PostAsJsonAsync("api/users/login", user);
        return GetResponseContent(response).Result.AccessToken;
    }

    public static async Task<Job?> RequestJobAsync(string accessToken)
    {
        var client = GetHttpClient(accessToken);
        var response = await client.GetAsync("api/dna/jobs");
        return GetResponseContent(response).Result.Job;
    }

    public static async Task<Response> SubmitEncodeStrandAsync(string accessToken, string jobId, Result result)
    {
        var client = GetHttpClient(accessToken);
        var response = await client.PostAsJsonAsync($"api/dna/jobs/{jobId}/encode", result);
        return await GetResponseContent(response);
    }

    public static async Task<Response> SubmitDecodeStrandAsync(string accessToken, string jobId, Result result)
    {
        var client = GetHttpClient(accessToken);
        var response = await client.PostAsJsonAsync($"api/dna/jobs/{jobId}/decode", result);
        return await GetResponseContent(response);
    }

    public static async Task<Response> SubmitCheckGeneAsync(string accessToken, string jobId, Result result)
    {
        var client = GetHttpClient(accessToken);
        var response = await client.PostAsJsonAsync($"api/dna/jobs/{jobId}/check", result);
        return await GetResponseContent(response);
    }

    private static HttpClient GetHttpClient(string accessToken = "")
    {
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        if (accessToken != "")
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        return _client;
    }

    private static async Task<Response> GetResponseContent(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpException(
                $"Failed to send the request.\nStatus code: {response.StatusCode}\nResponse: {response.Content.ReadAsStringAsync().Result}\n");
        }

        var responseContent = await response.Content.ReadFromJsonAsync<Response>();
        if (responseContent.Code != "Success")
        {
            throw new ApiException(
                $"Failed to get/submit data.\nCode: {responseContent.Code}\nMessage: {responseContent.Message}");
        }

        return responseContent;
    }
}