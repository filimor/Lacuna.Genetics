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
    ///     Get a Job from the server, handle it according to its type, and send the result to the client.
    /// </summary>
    /// <returns>A Tuple with the Response from the server and the Result sent to it.</returns>
    public Tuple<Response, Result> DoJob()
    {
        var job = GetJobAsync().Result;
        var (response, result) = HandleJobAsync(job).Result;
        response.Job = job;
        return new Tuple<Response, Result>(response, result);
    }

    /// <summary>
    ///     Get a new job from the server.
    /// </summary>
    /// <returns>The job retrieved.</returns>
    private async Task<Job> GetJobAsync()
    {
        return await _httpService.RequestJobAsync();
    }

    /// <summary>
    ///     Call the proper method on the LabService and send the result to the proper method on the HttpService, according to
    ///     the job type.
    /// </summary>
    /// <param name="job">The job to be processed.</param>
    /// <returns>A Tuple with the Response from the server and the Result sent to it.</returns>
    private async Task<Tuple<Response, Result>> HandleJobAsync(Job job)
    {
        var result = new Result
        {
            StrandEncoded = _labService.EncodeStrand(job.Strand!),
            Strand = _labService.DecodeStrand(job.StrandEncoded!),
            IsActivated = _labService.CheckGene(job.StrandEncoded!, job.GeneEncoded!)
        };

        var endpoint = JobType.GetEndpoint(job.Type, job.Id);
        var response = await _httpService.SubmitJobAsync(endpoint, result);
        return new Tuple<Response, Result>(response, result);
    }
}