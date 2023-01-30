using System.Text;

namespace Lacuna.Genetics.Core.Models;

public class Job
{
    [NonSerialized] public const string EncodeStrand = "EncodeStrand";
    [NonSerialized] public const string DecodeStrand = "DecodeStrand";
    [NonSerialized] public const string CheckGene = "CheckGene";

    [NonSerialized]
    private static readonly Dictionary<string, string> Endpoints = new()
    {
        { EncodeStrand, "api/dna/jobs/{0}/encode" },
        { DecodeStrand, "api/dna/jobs/{0}/decode" },
        { CheckGene, "api/dna/jobs/{0}/gene" }
    };

    public string Id { get; set; } = default!;
    public string Type { get; set; } = default!;
    public string? Strand { get; set; }
    public string? StrandEncoded { get; set; }
    public string? GeneEncoded { get; set; }

    public static string GetEndpoint(string type, string jobId)
    {
        return Endpoints[type].Replace("{0}", jobId);
    }

    public override string ToString()
    {
        var sb = new StringBuilder();

        sb.AppendLine($"JOB ID: {Id}");
        sb.AppendLine($"JOB TYPE: {Type}\n");

        if (Strand != null)
        {
            sb.AppendLine($"STRAND:\n{Strand}");
        }

        if (StrandEncoded != null)
        {
            sb.AppendLine($"STRAND ENCODED:\n{StrandEncoded}");
        }

        if (GeneEncoded != null)
        {
            sb.AppendLine($"\nGENE ENCODED:\n{GeneEncoded}");
        }

        return sb.ToString();
    }
}