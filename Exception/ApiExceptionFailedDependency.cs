using System.Net;
using Swashbuckle.AspNetCore.Filters;
using SyncStream.Exception.Api.Model;

// Define our namespace
namespace SyncStream.Exception.Api.Exception;

/// <summary>
///     This class maintains the structure of our 424 - Failed Dependency API error
/// </summary>
public class ApiExceptionFailedDependency : ApiException, IExamplesProvider<ApiExceptionModel>
{
    /// <summary>
    ///     This method converts a system <paramref name="exception" /> to a 424 - Failed Dependency API error
    /// </summary>
    /// <param name="exception">The system exception to convert</param>
    /// <returns></returns>
    public static ApiExceptionFailedDependency FromSystemException(System.Exception exception) =>
        ApiException.FromSystemException<ApiExceptionFailedDependency>(exception, HttpStatusCode.FailedDependency);

    /// <summary>
    ///     This method instantiates a empty throwable 405 - Method Not Allowed API error
    /// </summary>
    public ApiExceptionFailedDependency() : base(HttpStatusCode.FailedDependency)
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

    /// <summary>
    ///     This method generates a example instance of this type
    /// </summary>
    /// <returns>The example instance of this type</returns>
    public override ApiExceptionModel GetExamples() =>
        GetExamples(HttpStatusCode.FailedDependency, "424 - Failed Dependency");
}
