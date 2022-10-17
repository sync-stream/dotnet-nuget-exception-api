using System.Net;

// Define our namespace
namespace SyncStream.Exception.Api.Exception;

/// <summary>
///     This class maintains the structure of our 408 - Request Timeout API error
/// </summary>
public class ApiExceptionRequestTimeout : ApiException
{
    /// <summary>
    ///     This method instantiates an empty 408 - Request Timeout API error
    /// </summary>
    public ApiExceptionRequestTimeout() : base(HttpStatusCode.RequestTimeout)
    {
    }

    /// <summary>
    ///     This method instantiates a 408 - Request Timeout API exception from an existing exception
    /// </summary>
    /// <param name="exception">The existing exception</param>
    public ApiExceptionRequestTimeout(System.Exception exception) : base(HttpStatusCode.RequestTimeout, exception)
    {
    }

    /// <summary>
    ///     This method instantiates a throwable 408 - Request Timeout API error with a <paramref name="message" /> and optional <paramref name="innerException" />
    /// </summary>
    /// <param name="message">The message describing what happened</param>
    /// <param name="innerException">Optional, exception before this one</param>
    public ApiExceptionRequestTimeout(string message, System.Exception innerException = null) : base(
        HttpStatusCode.RequestTimeout, message, innerException)
    {
    }
}
