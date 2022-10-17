using System.Net;

// Define our namespace
namespace SyncStream.Exception.Api.Exception;

/// <summary>
///     This class maintains the structure of our 404 - Not Found API error
/// </summary>
public class ApiExceptionNotFound : ApiException
{
    /// <summary>
    ///     This method instantiates an empty 404 - Not Found API error
    /// </summary>
    public ApiExceptionNotFound() : base(HttpStatusCode.NotFound)
    {
    }

    /// <summary>
    ///     This method instantiates a 404 - Not Found API exception from an existing exception
    /// </summary>
    /// <param name="exception">The existing exception</param>
    public ApiExceptionNotFound(System.Exception exception) : base(HttpStatusCode.NotFound, exception)
    {
    }

    /// <summary>
    ///     This method instantiates a throwable 404 - Not Found API error with a <paramref name="message" /> and optional <paramref name="innerException" />
    /// </summary>
    /// <param name="message">The message describing what happened</param>
    /// <param name="innerException">Optional, exception before this one</param>
    public ApiExceptionNotFound(string message, System.Exception innerException = null) : base(HttpStatusCode.NotFound,
        message, innerException)
    {
    }
}
