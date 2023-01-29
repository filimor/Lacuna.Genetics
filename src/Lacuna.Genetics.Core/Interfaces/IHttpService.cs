using Lacuna.Genetics.Core.Models;

namespace Lacuna.Genetics.Core.Interfaces;

public interface IHttpService
{
    Task<Job> RequestJobAsync();
    Task<Response> SubmitEncodeStrandAsync(string jobId, Result result);
    Task<Response> SubmitDecodeStrandAsync(string jobId, Result result);
    Task<Response> SubmitCheckGeneAsync(string jobId, Result result);
}