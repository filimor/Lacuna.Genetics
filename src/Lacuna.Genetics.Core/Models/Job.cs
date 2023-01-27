namespace Lacuna.Genetics.Core.Models;

public class Job
{
    public string Id { get; set; } = default!;
    public string Type { get; set; } = default!;
    public string? Strand { get; set; }
    public string? StrandEncoded { get; set; }
    public string? GeneEncoded { get; set; }
}