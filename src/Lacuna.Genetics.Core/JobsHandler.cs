using Lacuna.Genetics.Core.Interfaces;
using Lacuna.Genetics.Core.Models;

namespace Lacuna.Genetics.Core;

public class JobsHandler
{
    private readonly User _user;
    private string _accessToken = string.Empty;
    private DateTime _accessTokenExpiration;
    private readonly ILaboratory _laboratory;

    // TODO: Allow user creation?
    public JobsHandler(User user, ILaboratory laboratory)
    {
        _user = user;
        _laboratory = laboratory;
    }

    // TODO: Implement parallel processing of jobs
    public async Task<Response> DoJobAsync()
    {
        var job = GetJob();
        var response = await HandleJobAsync(job);
        response.Job = job;
        return response;
    }

    public Job? GetJob()
    {
        RefreshAccessToken();
        return HttpService.RequestJobAsync(_accessToken).Result;
    }

    public async Task<Response> HandleJobAsync(Job job)
    {
        switch (job.Type)
        {
            case "EncodeStrand":
                var encodedStrand = _laboratory.EncodeStrand(job.Strand);
                return await HttpService.SubmitEncodeStrandAsync(_accessToken, job.Id,
                    new Result { StrandEncoded = encodedStrand });
            case "DecodeStrand":
                var decodedStrand = _laboratory.DecodeStrand(job.StrandEncoded);
                return await HttpService.SubmitDecodeStrandAsync(_accessToken, job.Id,
                    new Result { Strand = decodedStrand });
            case "CheckGene":
                var isActivated = _laboratory.CheckGene(job.StrandEncoded, job.GeneEncoded);
                return await HttpService.SubmitCheckGeneAsync(_accessToken, job.Id,
                    new Result { IsActivated = isActivated });
            default:
                return null;
        }
    }

    private void RefreshAccessToken()
    {
        if (!string.IsNullOrEmpty(_accessToken) && _accessTokenExpiration >= DateTime.Now)
        {
            return;
        }

        _accessToken = HttpService.RequestAccessTokenAsync(_user).Result;
        _accessTokenExpiration = DateTime.Now.AddMinutes(2);
    }
}