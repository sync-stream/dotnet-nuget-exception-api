using System.Net;

// Define our namespace
namespace SyncStream.Exception.Api.Exception;

/// <summary>
///     This class maintains the structure of our 405 - Method Not Allowed API error
/// </summary>
public class ApiExceptionMethodNotAllowed : ApiException
{
    /// <summary>
    ///     This method instantiates an empty 405 - Method Not Allowed API error
    /// </summary>
    public ApiExceptionMethodNotAllowed() : base(HttpStatusCode.MethodNotAllowed)
    {
    }

    /// <summary>
    ///     This method instantiates a 405 - Method Not Allowed API exception from an existing exception
    /// </summary>
    /// <param name="exception">The existing exception</param>
    public ApiExceptionMethodNotAllowed(System.Exception exception) : base(HttpStatusCode.MethodNotAllowed, exception)
    {
    }

    /// <summary>
    ///     This method instantiates a throwable 405 - Method Not Allowed API error with a <paramref name="message" /> and optional <paramref name="innerException" />
    /// </summary>
    /// <param name="message">The message describing what happened</param>
    /// <param name="innerException">Optional, exception before this one</param>
    public ApiExceptionMethodNotAllowed(string message, System.Exception innerException = null) : base(
        HttpStatusCode.MethodNotAllowed, message, innerException)
    {
    }
}
