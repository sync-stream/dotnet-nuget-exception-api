using System.Net;
using Swashbuckle.AspNetCore.Filters;

// Define our namespace
namespace SyncStream.Exception.Api.Exception;

/// <summary>
///     This class maintains the structure of our 424 - Failed Dependency API error
/// </summary>
public class ApiExceptionFailedDependency : ApiException, IExamplesProvider<ApiExceptionFailedDependency>
{
    /// <summary>
    ///     This method converts a system <paramref name="exception" /> to a 424 - Failed Dependency API error
    /// </summary>
    /// <param name="exception">The system exception to convert</param>
    /// <returns></returns>
    public static ApiExceptionFailedDependency FromSystemException(System.Exception exception) =>
        ApiException.FromSystemException<ApiExceptionFailedDependency>(exception, HttpStatusCode.FailedDependency);

    /// <summary>
    ///     This method instantiates an empty 405 - Method Not Allowed API error
    /// </summary>
    public  ApiExceptionFailedDependency() : base(HttpStatusCode.FailedDependency) { }

    /// <summary>
    ///     This method generates a example instance of this type
    /// </summary>
    /// <returns>The example instance of this type</returns>
    public new ApiExceptionFailedDependency GetExamples() =>
        GetExamples<ApiExceptionFailedDependency>(HttpStatusCode.FailedDependency, "424 - Failed Dependency");
}
