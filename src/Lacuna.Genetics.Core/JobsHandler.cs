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
    public async Task<Tuple<Response, Result>> DoJobAsync()
    {
        var job = GetJob();
        var response = await HandleJobAsync(job);
        response.Item1.Job = job;
        return response;
    }

    /// <summary>
    ///     Get a new job from the server.
    /// </summary>
    /// <returns>The job retrieved.</returns>
    private Job GetJob()
    {
        return _httpService.RequestJobAsync().Result;
    }

    /// <summary>
    ///     Call the proper method on the Laboratory and send the result to the proper method on the HttpService, according to
    ///     the job type.
    /// </summary>
    /// <param name="job">The job to be processed.</param>
    /// <returns>A Tuple with the Response from the server and the Result sent to it.</returns>
    /// <exception cref="Exception">Thrown if the job type is unknown.</exception>
    private async Task<Tuple<Response, Result>> HandleJobAsync(Job job)
    {
        Response response;
        Result result;

        switch (job.Type)
        {
            case JobType.EncodeStrand:
                var encodedStrand = _laboratory.EncodeStrand(job.Strand!);
                result = new Result { StrandEncoded = encodedStrand };
                response = await _httpService.SubmitEncodeStrandAsync(job.Id,
                    result);
                return new Tuple<Response, Result>(response, result);
            case JobType.DecodeStrand:
                var decodedStrand = _laboratory.DecodeStrand(job.StrandEncoded!);
                result = new Result { Strand = decodedStrand };
                response = await _httpService.SubmitDecodeStrandAsync(job.Id,
                    result);
                return new Tuple<Response, Result>(response, result);
            case JobType.CheckGene:
                var isActivated = _laboratory.CheckGene(job.StrandEncoded!, job.GeneEncoded!);
                result = new Result { IsActivated = isActivated };
                response = await _httpService.SubmitCheckGeneAsync(job.Id,
                    result);
                return new Tuple<Response, Result>(response, result);
            default:
                throw new Exception("Unknown job type");
        }
    }
}