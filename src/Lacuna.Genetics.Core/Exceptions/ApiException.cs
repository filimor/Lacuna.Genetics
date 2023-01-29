namespace Lacuna.Genetics.Core.Exceptions;

/// <summary>
///     The ApiException is thrown when the API doesn't return a 'Success' code in the response body.
/// </summary>
public class ApiException : Exception
{
    private readonly string? _responseCode;
    private readonly string? _responseMessage;

    public ApiException()
    {
    }

    public ApiException(string message) : base(message)
    {
    }

    public ApiException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public ApiException(string responseCode, string responseMessage)
    {
        _responseCode = responseCode;
        _responseMessage = responseMessage;
    }

    public override string Message => $"Failed to get/submit data.\nCode: {_responseCode}\nMessage: {_responseMessage}";
}