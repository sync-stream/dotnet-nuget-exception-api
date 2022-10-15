using System.Net;
using Swashbuckle.AspNetCore.Filters;

// Define our namespace
namespace SyncStream.Exception.Api.Exception;

/// <summary>
///     This class maintains the structure of our 400 - Bad Request API error
/// </summary>
public class ApiExceptionBadRequest : ApiException, IExamplesProvider<ApiExceptionBadRequest>
{
    /// <summary>
    ///     This method converts a system <paramref name="exception" /> to a 400 - Bad Request API error
    /// </summary>
    /// <param name="exception">The system exception to convert</param>
    /// <returns></returns>
    public static ApiExceptionBadRequest FromSystemException(System.Exception exception) =>
        ApiException.FromSystemException<ApiExceptionBadRequest>(exception, HttpStatusCode.BadRequest);

    /// <summary>
    ///     This method instantiates an empty 400 - Bad Request API error
    /// </summary>
    public ApiExceptionBadRequest() : base(HttpStatusCode.BadRequest)
    {
    }

    /// <summary>
    ///     This method generates a example instance of this type
    /// </summary>
    /// <returns>The example instance of this type</returns>
    public new ApiExceptionBadRequest GetExamples() =>
        GetExamples<ApiExceptionBadRequest>(HttpStatusCode.BadRequest, "400 - Bad Request");
}
