using Lacuna.Genetics.Core.Interfaces;
using Lacuna.Genetics.Core.Models;

namespace Lacuna.Genetics.Core;

public class JobsHandler
{
    private readonly IHttpService _httpService;
    private readonly ILaboratory _laboratory;

    public JobsHandler(ILaboratory laboratory, IHttpService httpService)
    {
        _laboratory = laboratory;
        _httpService = httpService;
    }

    /// <summary>
    ///     Get a Job from the server, handle it according to its type, and send the result to the client.
    /// </summary>
    /// <returns>A Tuple with the Response from the server and the Result sent to it.</returns>
    public Tuple<Response, Result> DoJob()
    {
        var job = GetJobAsync().Result;
        var response = HandleJobAsync(job).Result;
        response.Item1.Job = job;
        return response;
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
    ///     Call the proper method on the Laboratory and send the result to the proper method on the HttpService, according to
    ///     the job type.
    /// </summary>
    /// <param name="job">The job to be processed.</param>
    /// <returns>A Tuple with the Response from the server and the Result sent to it.</returns>
    private async Task<Tuple<Response, Result>> HandleJobAsync(Job job)
    {
        var encodedStrand = _laboratory.EncodeStrand(job.Strand!);
        var decodedStrand = _laboratory.DecodeStrand(job.StrandEncoded!);
        var isActivated = _laboratory.CheckGene(job.StrandEncoded!, job.GeneEncoded!);

        var result = new Result
        {
            StrandEncoded = encodedStrand,
            Strand = decodedStrand,
            IsActivated = isActivated
        };

        var response = await _httpService.SubmitJobAsync(JobType.GetEndpoint(job.Type, job.Id), result);
        return new Tuple<Response, Result>(response, result);
    }
}