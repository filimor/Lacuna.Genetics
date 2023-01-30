using System.Net;
using Lacuna.Genetics.Core.Interfaces;
using Lacuna.Genetics.Core.Models;

namespace Lacuna.Genetics.Core;

public class JobsHandler
{
    private readonly IHttpService _httpService;
    private readonly ILabService _labService;

    public JobsHandler(ILabService labService, IHttpService httpService)
    {
        _labService = labService;
        _httpService = httpService;
    }

    /// <summary>
    ///     Get a Job from the server and send it to the LabService to be analyzed.
    ///     After that, send the result to the server and return it to the client.
    /// </summary>
    /// <returns>A Tuple with the Response from the server and the Result sent to it.</returns>
    public async Task<Tuple<Response, Result>> DoJobAsync()
    {
        var job = await GetJobAsync();
        var endpoint = Job.GetEndpoint(job.Type, job.Id);
        var result = HandleJob(job);
        var response = await SendJobAsync(endpoint, result);
        response.Job = job;
        
        return new Tuple<Response, Result>(response, result);
    }

    private async Task<Job> GetJobAsync()
    {
        return await _httpService.RequestJobAsync();
    }

    private Result HandleJob(Job job)
    {
        return _labService.Analyze(job);
    }

    private async Task<Response> SendJobAsync(string endpoint, Result result)
    {
        return await _httpService.SubmitJobAsync(endpoint, result);
    }
}