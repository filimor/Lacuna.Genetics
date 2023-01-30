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
        BaseAddress = new Uri("https://gene.lacuna.cc/"),
        DefaultRequestHeaders = { Accept = { new MediaTypeWithQualityHeaderValue("application/json") } }
    };

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    private static string _accessToken = string.Empty;
    private static DateTime _accessTokenExpiration;
    private readonly User _user;

    public HttpService(User user)
    {
        _user = user;
    }

    /// <summary>
    ///     Request a new job from the server.
    /// </summary>
    /// <returns>A Job object with the job's data.</returns>
    public async Task<Job> RequestJobAsync()
    {
        await GetAuthorizationAsync();
        var response = await Client.GetAsync("api/dna/jobs");
        return GetResponseContentAsync(response).Result.Job!;
    }

    /// <summary>
    ///     Send the result of a job to the server.
    /// </summary>
    /// <param name="endpoint">The endpoint to send the result.</param>
    /// <param name="result">The Result object with the parameters to send.</param>
    /// <returns>The response gotten.</returns>
    public async Task<Response> SubmitJobAsync(string endpoint, Result result)
    {
        await GetAuthorizationAsync();
        var response = await Client.PostAsJsonAsync(endpoint,
            result, JsonOptions);
        return await GetResponseContentAsync(response);
    }

    /// <summary>
    ///     Refresh the Token and set the Authorization header. It means to be used BEFORE each request.
    /// </summary>
    private async Task GetAuthorizationAsync()
    {
        if (!string.IsNullOrEmpty(_accessToken) && _accessTokenExpiration >= DateTime.Now)
        {
            return;
        }

        var response = await Client.PostAsJsonAsync("api/users/login", _user);
        _accessToken = GetResponseContentAsync(response).Result.AccessToken!;
        _accessTokenExpiration = DateTime.Now.AddMinutes(2);
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
    }

    /// <summary>
    ///     Get the response of an HttpRequest and deserialize it to a Response object.
    ///     It means to be used AFTER each request.
    /// </summary>
    /// <param name="response">The HttpResponseMessage object</param>
    /// <returns></returns>
    /// <exception cref="HttpException">Thrown if the response doesn't have a success code (200-299).</exception>
    /// <exception cref="ApiException">Thrown if the 'Code' attribute of the response body isn't Success.</exception>
    private static async Task<Response> GetResponseContentAsync(HttpResponseMessage response)
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