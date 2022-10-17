using System.Net;

// Define our namespace
namespace SyncStream.Exception.Api.Exception;

/// <summary>
///     This class maintains the structure of our 424 - Failed Dependency API error
/// </summary>
public class ApiExceptionFailedDependency : ApiException
{
    /// <summary>
    ///     This method instantiates a empty throwable 405 - Method Not Allowed API error
    /// </summary>
    public ApiExceptionFailedDependency() : base(HttpStatusCode.FailedDependency)
    {
    }

    /// <summary>
    ///     This method instantiates a 424 - Failed Dependency API exception from an existing exception
    /// </summary>
    /// <param name="exception">The existing exception</param>
    public ApiExceptionFailedDependency(System.Exception exception) : base(HttpStatusCode.FailedDependency, exception)
    {
    }

    /// <summary>
    ///     This method instantiates a throwable 424 - Failed Dependency API error with a <paramref name="message" /> and optional <paramref name="innerException" />
    /// </summary>
    /// <param name="message">The message describing what happened</param>
    /// <param name="innerException">Optional, exception before this one</param>
    public ApiExceptionFailedDependency(string message, System.Exception innerException = null) : base(
        HttpStatusCode.FailedDependency, message, innerException)
    {
    }
}
