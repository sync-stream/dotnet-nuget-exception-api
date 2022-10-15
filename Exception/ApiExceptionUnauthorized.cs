using System.Net;
using Swashbuckle.AspNetCore.Filters;

// Define our namespace
namespace SyncStream.Exception.Api.Exception;

/// <summary>
///     This class maintains the structure of our 401 - Unauthorized API error
/// </summary>
public class ApiExceptionUnauthorized : ApiException, IExamplesProvider<ApiExceptionUnauthorized>
{
    /// <summary>
    ///     This method converts a system <paramref name="exception" /> to a 401 - Unauthorized API error
    /// </summary>
    /// <param name="exception">The system exception to convert</param>
    /// <returns></returns>
    public static ApiExceptionUnauthorized FromSystemException(System.Exception exception) =>
        ApiException.FromSystemException<ApiExceptionUnauthorized>(exception, HttpStatusCode.Unauthorized);

    /// <summary>
    ///     This method instantiates an empty 404 - Not Found API error
    /// </summary>
    public ApiExceptionUnauthorized() : base(HttpStatusCode.Unauthorized)
    {
    }

    /// <summary>
    ///     This method generates a example instance of this type
    /// </summary>
    /// <returns>The example instance of this type</returns>
    public new ApiExceptionUnauthorized GetExamples() =>
        GetExamples<ApiExceptionUnauthorized>(HttpStatusCode.Unauthorized, "401 - Unauthorized");
}