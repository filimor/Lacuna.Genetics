namespace Lacuna.Genetics.Core.Models;

public class Response
{
    public string Code { get; set; } = default!;
    public string Message { get; set; } = default!;
    public string? AccessToken { get; set; }
    public Job? Job { get; set; }
}