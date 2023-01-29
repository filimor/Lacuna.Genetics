using System.Net;

namespace Lacuna.Genetics.Core.Exceptions;

/// <summary>
/// The HttpException is thrown when the HTTP request doesn't return a success status code.
/// </summary>
public class HttpException : Exception
{
    private readonly string? _responseContent;
    private readonly HttpStatusCode _statusCode;

    public HttpException()
    {
    }

    public HttpException(string message) : base(message)
    {
    }

    public HttpException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public HttpException(HttpStatusCode statusCode, string responseContent)
    {
        _statusCode = statusCode;
        _responseContent = responseContent;
    }

    public override string Message =>
        $"Failed to send the request.\nStatus code: {_statusCode}\nResponse: {_responseContent}\n";
}