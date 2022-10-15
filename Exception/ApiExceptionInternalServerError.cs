using System.Net;
using Swashbuckle.AspNetCore.Filters;

// Define our namespace
namespace SyncStream.Exception.Api.Exception;

/// <summary>
///     This class maintains the structure of our 500 - Internal Server Error API error
/// </summary>
public class ApiExceptionInternalServerError : ApiException, IExamplesProvider<ApiExceptionInternalServerError>
{
    /// <summary>
    ///     This method converts a system <paramref name="exception" /> to a 500 - Internal Server Error API error
    /// </summary>
    /// <param name="exception">The system exception to convert</param>
    /// <returns></returns>
    public static ApiExceptionInternalServerError FromSystemException(System.Exception exception) =>
        ApiException.FromSystemException<ApiExceptionInternalServerError>(exception);

    /// <summary>
    ///     This method instantiates an empty 500 - Internal Server Error API error
    /// </summary>
    public ApiExceptionInternalServerError() : base(HttpStatusCode.InternalServerError)
    {
    }

    /// <summary>
    ///     This method generates a example instance of this type
    /// </summary>
    /// <returns>The example instance of this type</returns>
    public new ApiExceptionInternalServerError GetExamples() =>
        GetExamples<ApiExceptionInternalServerError>(HttpStatusCode.InternalServerError, "500 - Internal Server Error");
}
