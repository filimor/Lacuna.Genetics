namespace Lacuna.Genetics.Core.Exceptions;

public class HttpException : Exception
{
    public HttpException()
    {
    }

    public HttpException(string message) : base(message)
    {
    }

    public HttpException(string message, Exception innerException) : base(message, innerException)
    {
    }
}