using System.Net;

// Define our namespace
namespace SyncStream.Exception.Api.Exception;

/// <summary>
///     This class maintains the structure of our 415 - Unsupported Media Type API error
/// </summary>
public class ApiExceptionUnsupportedMediaType : ApiException
{
    /// <summary>
    ///     This method instantiates an empty 415 - Unsupported Media Type API error
    /// </summary>
    public ApiExceptionUnsupportedMediaType() : base(HttpStatusCode.UnsupportedMediaType)
    {
    }

    /// <summary>
    ///     This method instantiates a 415 - Unsupported Media Type API exception from an existing exception
    /// </summary>
    /// <param name="exception">The existing exception</param>
    public ApiExceptionUnsupportedMediaType(System.Exception exception) : base(HttpStatusCode.UnsupportedMediaType,
        exception)
    {
    }

    /// <summary>
    ///     This method instantiates a throwable 415 - Unsupported Media Type API error with a <paramref name="message" /> and optional <paramref name="innerException" />
    /// </summary>
    /// <param name="message">The message describing what happened</param>
    /// <param name="innerException">Optional, exception before this one</param>
    public ApiExceptionUnsupportedMediaType(string message, System.Exception innerException = null) : base(
        HttpStatusCode.UnsupportedMediaType, message, innerException)
    {
    }
}
