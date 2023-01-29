namespace Lacuna.Genetics.Core.Models;

public class JobType
{
    public const string EncodeStrand = "EncodeStrand";
    public const string DecodeStrand = "DecodeStrand";
    public const string CheckGene = "CheckGene";

    public static string GetEndpoint(string type, string jobId)
    {
        return type switch
        {
            EncodeStrand => $"api/dna/jobs/{jobId}/encode",
            DecodeStrand => $"api/dna/jobs/{jobId}/decode",
            CheckGene => $"api/dna/jobs/{jobId}/gene",
            _ => throw new Exception("Unknown job type.")
        };
    }
}