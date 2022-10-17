using System.Net;

// Define our namespace
namespace SyncStream.Exception.Api.Exception;

/// <summary>
///     This class maintains the structure of our 500 - Internal Server Error API error
/// </summary>
public class ApiExceptionInternalServerError : ApiException
{
    /// <summary>
    ///     This method instantiates an empty 500 - Internal Server Error API error
    /// </summary>
    public ApiExceptionInternalServerError() : base(HttpStatusCode.InternalServerError)
    {
    }

    /// <summary>
    ///     This method instantiates a 500 - Internal Server Error API exception from an existing exception
    /// </summary>
    /// <param name="exception">The existing exception</param>
    public ApiExceptionInternalServerError(System.Exception exception) : base(HttpStatusCode.InternalServerError,
        exception)
    {
    }

    /// <summary>
    ///     This method instantiates a throwable 500 - Internal Service Error API error with a <paramref name="message" /> and optional <paramref name="innerException" />
    /// </summary>
    /// <param name="message">The message describing what happened</param>
    /// <param name="innerException">Optional, exception before this one</param>
    public ApiExceptionInternalServerError(string message, System.Exception innerException = null) : base(
        HttpStatusCode.InternalServerError, message, innerException)
    {
    }
}
