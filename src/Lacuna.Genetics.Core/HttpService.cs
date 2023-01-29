using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
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

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };


    public async Task<string> RequestAccessTokenAsync(User user)
    {
        GetHttpClient();
        var response = await Client.PostAsJsonAsync("api/users/login", user);
        return GetResponseContent(response).Result.AccessToken!;
    }

    public async Task<Job> RequestJobAsync(string accessToken)
    {
        GetHttpClient(accessToken);
        var response = await Client.GetAsync("api/dna/jobs");
        return GetResponseContent(response).Result.Job!;
    }

    public async Task<Response> SubmitEncodeStrandAsync(string accessToken, string jobId, Result result)
    {
        GetHttpClient(accessToken);
        var response = await Client.PostAsJsonAsync($"api/dna/jobs/{jobId}/encode",
            result, JsonOptions);
        return await GetResponseContent(response);
    }

    public async Task<Response> SubmitDecodeStrandAsync(string accessToken, string jobId, Result result)
    {
        GetHttpClient(accessToken);
        var response = await Client.PostAsJsonAsync($"api/dna/jobs/{jobId}/decode",
            result, JsonOptions);
        return await GetResponseContent(response);
    }

    public async Task<Response> SubmitCheckGeneAsync(string accessToken, string jobId, Result result)
    {
        GetHttpClient(accessToken);
        var response = await Client.PostAsJsonAsync($"api/dna/jobs/{jobId}/gene",
            result, JsonOptions);
        return await GetResponseContent(response);
    }

    /// <summary>
    ///     Get the HttpClient from the class, clear the headers and add the Accept headers.
    ///     It means to be used BEFORE each request.
    /// </summary>
    private static void GetHttpClient()
    {
        Client.DefaultRequestHeaders.Accept.Clear();
        Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    /// <summary>
    ///     Get the HttpClient from the class, clear the headers and add the Accept and Authorization headers.
    ///     It means to be used BEFORE each request.
    /// </summary>
    /// <param name="accessToken">The Access Token.</param>
    private static void GetHttpClient(string accessToken)
    {
        GetHttpClient();
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
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