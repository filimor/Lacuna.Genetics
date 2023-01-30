using System.Text;

namespace Lacuna.Genetics.Core.Models;

public class Job
{
    public string Id { get; set; } = default!;
    public string Type { get; set; } = default!;
    public string? Strand { get; set; }
    public string? StrandEncoded { get; set; }
    public string? GeneEncoded { get; set; }

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