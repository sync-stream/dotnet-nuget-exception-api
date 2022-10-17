using System.Net;

// Define our namespace
namespace SyncStream.Exception.Api.Exception;

/// <summary>
///     This class maintains the structure of our 409 - Conflict API error
/// </summary>
public class ApiExceptionConflict : ApiException
{
    /// <summary>
    ///     This method instantiates an empty 409 - Conflict API error
    /// </summary>
    public ApiExceptionConflict() : base(HttpStatusCode.Conflict)
    {
    }

    /// <summary>
    ///     This method instantiates a 409 - Conflict API exception from an existing exception
    /// </summary>
    /// <param name="exception">The existing exception</param>
    public ApiExceptionConflict(System.Exception exception) : base(HttpStatusCode.Conflict, exception)
    {
    }

    /// <summary>
    ///     This method instantiates a throwable 409 - Conflict API error with a <paramref name="message" /> and optional <paramref name="innerException" />
    /// </summary>
    /// <param name="message">The message describing what happened</param>
    /// <param name="innerException">Optional, exception before this one</param>
    public ApiExceptionConflict(string message, System.Exception innerException = null) : base(
        HttpStatusCode.Conflict, message, innerException)
    {
    }
}
