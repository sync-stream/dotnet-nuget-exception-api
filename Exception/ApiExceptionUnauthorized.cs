using System.Net;

// Define our namespace
namespace SyncStream.Exception.Api.Exception;

/// <summary>
///     This class maintains the structure of our 401 - Unauthorized API error
/// </summary>
public class ApiExceptionUnauthorized : ApiException
{
    /// <summary>
    ///     This method instantiates an empty 404 - Not Found API error
    /// </summary>
    public ApiExceptionUnauthorized() : base(HttpStatusCode.Unauthorized)
    {
    }

    /// <summary>
    ///     This method instantiates a 401 - Unauthorized API exception from an existing exception
    /// </summary>
    /// <param name="exception">The existing exception</param>
    public ApiExceptionUnauthorized(System.Exception exception) : base(HttpStatusCode.Unauthorized, exception)
    {
    }

    /// <summary>
    ///     This method instantiates a throwable 401 - Unauthorized API error with a <paramref name="message" /> and optional <paramref name="innerException" />
    /// </summary>
    /// <param name="message">The message describing what happened</param>
    /// <param name="innerException">Optional, exception before this one</param>
    public ApiExceptionUnauthorized(string message, System.Exception innerException = null) : base(
        HttpStatusCode.Unauthorized, message, innerException)
    {
    }
}
