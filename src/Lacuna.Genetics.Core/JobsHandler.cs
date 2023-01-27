using Lacuna.Genetics.Core.Interfaces;
using Lacuna.Genetics.Core.Models;

namespace Lacuna.Genetics.Core;

public class JobsHandler
{
    private readonly IHttpService _httpService;
    private readonly ILaboratory _laboratory;
    private readonly User _user;
    private string _accessToken = string.Empty;
    private DateTime _accessTokenExpiration;

    // TODO: Allow user creation?
    public JobsHandler(User user, ILaboratory laboratory, IHttpService httpService)
    {
        _user = user;
        _laboratory = laboratory;
        _httpService = httpService;
    }

    // TODO: Implement parallel processing of jobs
    public async Task<Tuple<Response, Result>> DoJobAsync()
    {
        var job = GetJob();
        var response = await HandleJobAsync(job);
        response.Item1.Job = job;
        return response;
    }

    public Job? GetJob()
    {
        RefreshAccessToken();
        return _httpService.RequestJobAsync(_accessToken).Result;
    }

    public async Task<Tuple<Response, Result>> HandleJobAsync(Job job)
    {
        Response response;
        Result result;

        switch (job.Type)
        {
            case "EncodeStrand":
                var encodedStrand = _laboratory.EncodeStrand(job.Strand);
                result = new Result { StrandEncoded = encodedStrand };
                response = await _httpService.SubmitEncodeStrandAsync(_accessToken, job.Id,
                    result);
                return new Tuple<Response, Result>(response, result);
            case "DecodeStrand":
                var decodedStrand = _laboratory.DecodeStrand(job.StrandEncoded);
                result = new Result { Strand = decodedStrand };
                response = await _httpService.SubmitDecodeStrandAsync(_accessToken, job.Id,
                    result);
                return new Tuple<Response, Result>(response, result);
            case "CheckGene":
                var isActivated = _laboratory.CheckGene(job.StrandEncoded, job.GeneEncoded);
                result = new Result { IsActivated = isActivated };
                response = await _httpService.SubmitCheckGeneAsync(_accessToken, job.Id,
                    result);
                return new Tuple<Response, Result>(response, result);
            default:
                throw new Exception("Unknown job type");
        }
    }

    private void RefreshAccessToken()
    {
        if (!string.IsNullOrEmpty(_accessToken) && _accessTokenExpiration >= DateTime.Now)
        {
            return;
        }

        _accessToken = _httpService.RequestAccessTokenAsync(_user).Result;
        _accessTokenExpiration = DateTime.Now.AddMinutes(2);
    }
}