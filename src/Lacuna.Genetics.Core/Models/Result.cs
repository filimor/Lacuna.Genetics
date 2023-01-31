using System.Text;

namespace Lacuna.Genetics.Core.Models;

public class Result
{
    public string? Strand { get; set; }
    public string? StrandEncoded { get; set; }
    public bool? IsActivated { get; set; }

    public override string ToString()
    {
        var sb = new StringBuilder();

        if (Strand != null)
        {
            sb.AppendLine($"STRAND:\n{Strand}");
        }

        if (StrandEncoded != null)
        {
            sb.AppendLine($"STRAND ENCODED:\n{StrandEncoded}");
        }

        if (IsActivated != null)
        {
            sb.AppendLine($"IS ACTIVATED: {IsActivated}");
        }

        return sb.ToString();
    }
}