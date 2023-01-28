using System.Net.Http.Headers;
using System.Net.Http.Json;
using Lacuna.Genetics.Core.Exceptions;
using Lacuna.Genetics.Core.Interfaces;
using Lacuna.Genetics.Core.Models;

namespace Lacuna.Genetics.Core;

public class HttpService : IHttpService
{
    private static readonly HttpClient Client = new()
    {
        BaseAddress = new Uri("https://gene.lacuna.cc/")
    };

    public async Task<string> RequestAccessTokenAsync(User user)
    {
        var client = GetHttpClient();
        var response = await client.PostAsJsonAsync("api/users/login", user);
        return GetResponseContent(response).Result.AccessToken!;
    }

    public async Task<Job> RequestJobAsync(string accessToken)
    {
        var client = GetHttpClient(accessToken);
        var response = await client.GetAsync("api/dna/jobs");
        return GetResponseContent(response).Result.Job!;
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

    /// <summary>
    ///     Get the HttpClient from the class, clear the headers and add the Accept headers.
    ///     It means to be used BEFORE each request.
    /// </summary>
    /// <returns>The HttpClient with the proper headers.</returns>
    private static HttpClient GetHttpClient()
    {
        Client.DefaultRequestHeaders.Accept.Clear();
        Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        return Client;
    }

    /// <summary>
    ///     Get the HttpClient from the class, clear the headers and add the Accept and Authorization headers.
    ///     It means to be used BEFORE each request.
    /// </summary>
    /// <param name="accessToken">The Access Token.</param>
    /// <returns>The HttpClient with the proper headers.</returns>
    private static HttpClient GetHttpClient(string accessToken)
    {
        var client = GetHttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        return client;
    }

    /// <summary>
    ///     Get the response of an HttpRequest and deserialize it to a Response object.
    ///     It means to be used AFTER each request.
    /// </summary>
    /// <param name="response">The HttpResponseMessage object</param>
    /// <returns></returns>
    /// <exception cref="HttpException">Thrown if the response doesn't have a success code (200-299).</exception>
    /// <exception cref="ApiException">Thrown if the 'Code' attribute of the response body isn't Success.</exception>
    private static async Task<Response> GetResponseContent(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpException(response.StatusCode, response.Content.ReadAsStringAsync().Result);
        }

        var responseContent = await response.Content.ReadFromJsonAsync<Response>();

        return responseContent!.Code == Response.SuccessCode
            ? responseContent
            : throw new ApiException(responseContent.Code, responseContent.Message);
    }
}