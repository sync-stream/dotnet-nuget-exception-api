using System.Net;
using Swashbuckle.AspNetCore.Filters;
using SyncStream.Exception.Api.Model;

// Define our namespace
namespace SyncStream.Exception.Api.Exception;

/// <summary>
///     This class maintains the structure of our 400 - Bad Request API error
/// </summary>
public class ApiExceptionBadRequest : ApiException, IExamplesProvider<ApiExceptionModel>
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
    ///     This method instantiates a throwable 400 - Bad Request API error with a <paramref name="message" /> and optional <paramref name="innerException" />
    /// </summary>
    /// <param name="message">The message describing what happened</param>
    /// <param name="innerException">Optional, exception before this one</param>
    public ApiExceptionBadRequest(string message, System.Exception innerException = null) : base(
        HttpStatusCode.BadRequest, message, innerException)
    {
    }

    /// <summary>
    ///     This method generates a example instance of this type
    /// </summary>
    /// <returns>The example instance of this type</returns>
    public override ApiExceptionModel GetExamples() => GetExamples(HttpStatusCode.BadRequest, "400 - Bad Request");
}
