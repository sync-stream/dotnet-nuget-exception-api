using System.Net;

// Define our namespace
namespace SyncStream.Exception.Api.Exception;

/// <summary>
///     This class maintains the structure of our 501 - Not Implemented API error
/// </summary>
public class ApiExceptionNotImplemented : ApiException
{
    /// <summary>
    ///     This method instantiates an empty 501 - Not Implemented API error
    /// </summary>
    public ApiExceptionNotImplemented() : base(HttpStatusCode.NotImplemented)
    {
    }

    /// <summary>
    ///     This method instantiates a 501 - Not Implemented API exception from an existing exception
    /// </summary>
    /// <param name="exception">The existing exception</param>
    public ApiExceptionNotImplemented(System.Exception exception) : base(HttpStatusCode.NotImplemented, exception)
    {
    }

    /// <summary>
    ///     This method instantiates a throwable 501 - Not Implemented API error with a <paramref name="message" /> and optional <paramref name="innerException" />
    /// </summary>
    /// <param name="message">The message describing what happened</param>
    /// <param name="innerException">Optional, exception before this one</param>
    public ApiExceptionNotImplemented(string message, System.Exception innerException = null) : base(
        HttpStatusCode.NotImplemented, message, innerException)
    {
    }
}
