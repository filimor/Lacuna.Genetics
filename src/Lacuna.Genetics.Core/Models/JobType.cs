namespace Lacuna.Genetics.Core.Models;

public class JobType
{
    public const string EncodeStrand = "EncodeStrand";
    public const string DecodeStrand = "DecodeStrand";
    public const string CheckGene = "CheckGene";

    private static readonly Dictionary<string, string> Endpoints = new()
    {
        { EncodeStrand, "api/dna/jobs/{0}/encode" },
        { DecodeStrand, "api/dna/jobs/{0}/decode" },
        { CheckGene, "api/dna/jobs/{0}/gene" }
    };

    public static string GetEndpoint(string type, string jobId)
    {
        return !Endpoints.TryGetValue(type, out var endpoint)
            ? throw new Exception("Unknown job type.")
            : string.Format(endpoint, jobId);
    }
}