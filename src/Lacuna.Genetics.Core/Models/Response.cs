namespace Lacuna.Genetics.Core.Models;

public class Response
{
    [NonSerialized] public static readonly string SuccessCode = "Success";

    public string Code { get; set; } = default!;
    public string Message { get; set; } = default!;
    public string? AccessToken { get; set; }
    public Job? Job { get; set; }

    public override string ToString()
    {
        return $"JOB RESPONSE: {Code} {(!string.IsNullOrEmpty(Message) ? '-' : ' ')} {Message}\n{Job}";
    }
}