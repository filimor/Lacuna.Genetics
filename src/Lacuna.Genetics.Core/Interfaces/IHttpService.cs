using Lacuna.Genetics.Core.Models;

namespace Lacuna.Genetics.Core.Interfaces;

public interface IHttpService
{
    Task<string> RequestAccessTokenAsync(User user);
    Task<Job> RequestJobAsync(string accessToken);
    Task<Response> SubmitEncodeStrandAsync(string accessToken, string jobId, Result result);
    Task<Response> SubmitDecodeStrandAsync(string accessToken, string jobId, Result result);
    Task<Response> SubmitCheckGeneAsync(string accessToken, string jobId, Result result);
}