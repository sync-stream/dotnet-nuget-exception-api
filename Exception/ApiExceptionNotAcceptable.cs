using System.Net;

// Define our namespace
namespace SyncStream.Exception.Api.Exception;

/// <summary>
///     This class maintains the structure of our 406 - Not Acceptable API error
/// </summary>
public class ApiExceptionNotAcceptable : ApiException
{
    /// <summary>
    ///     This method instantiates an empty 406 - Not Acceptable API error
    /// </summary>
    public ApiExceptionNotAcceptable() : base(HttpStatusCode.NotAcceptable)
    {
    }

    /// <summary>
    ///     This method instantiates a 406 - Not Acceptable API exception from an existing exception
    /// </summary>
    /// <param name="exception">The existing exception</param>
    public ApiExceptionNotAcceptable(System.Exception exception) : base(HttpStatusCode.NotAcceptable, exception)
    {
    }

    /// <summary>
    ///     This method instantiates a throwable 406 - Not Acceptable API error with a <paramref name="message" /> and optional <paramref name="innerException" />
    /// </summary>
    /// <param name="message">The message describing what happened</param>
    /// <param name="innerException">Optional, exception before this one</param>
    public ApiExceptionNotAcceptable(string message, System.Exception innerException = null) : base(
        HttpStatusCode.NotAcceptable, message, innerException)
    {
    }
}
