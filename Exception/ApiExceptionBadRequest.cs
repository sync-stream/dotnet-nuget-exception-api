using System.Net;

// Define our namespace
namespace SyncStream.Exception.Api.Exception;

/// <summary>
///     This class maintains the structure of our 400 - Bad Request API error
/// </summary>
public class ApiExceptionBadRequest : ApiException
{
    /// <summary>
    ///     This method instantiates an empty 400 - Bad Request API error
    /// </summary>
    public ApiExceptionBadRequest() : base(HttpStatusCode.BadRequest)
    {
    }

    /// <summary>
    ///     This method instantiates a 400 - Bad Request API exception from an existing exception
    /// </summary>
    /// <param name="exception">The existing exception</param>
    public ApiExceptionBadRequest(System.Exception exception) : base(HttpStatusCode.BadRequest, exception)
    {
    }

    /// <summary>
    ///     This method instantiates a throwable 400 - Bad Request API error with a <paramref name="message" /> and optional <paramref name="innerException" />
    /// </summary>
    /// <param name="message">The message describing what happened</param>
    /// <param name="innerException">Optional, exception before this one</param>
    public ApiExceptionBadRequest(string message, System.Exception innerException = null) : base(
        HttpStatusCode.BadRequest, message, innerException)
    {
    }
}
