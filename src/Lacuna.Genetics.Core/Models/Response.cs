namespace Lacuna.Genetics.Core.Models;

public class Response
{
    public string Code { get; set; }
    public string Message { get; set; }
    public string? AccessToken { get; set; }
    public Job? Job { get; set; }
}