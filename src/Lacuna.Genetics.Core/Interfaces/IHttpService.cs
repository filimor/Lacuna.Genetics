using Lacuna.Genetics.Core.Models;

namespace Lacuna.Genetics.Core.Interfaces;

public interface IHttpService
{
    Task<Job> RequestJobAsync();
    Task<Response> SubmitJobAsync(string jobId, string jobEndpoint, Result result);
}