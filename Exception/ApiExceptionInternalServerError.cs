using System.Net;
using Swashbuckle.AspNetCore.Filters;
using SyncStream.Exception.Api.Model;

// Define our namespace
namespace SyncStream.Exception.Api.Exception;

/// <summary>
///     This class maintains the structure of our 500 - Internal Server Error API error
/// </summary>
public class ApiExceptionInternalServerError : ApiException, IExamplesProvider<ApiExceptionModel>
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
    ///     This method instantiates a throwable 500 - Internal Service Error API error with a <paramref name="message" /> and optional <paramref name="innerException" />
    /// </summary>
    /// <param name="message">The message describing what happened</param>
    /// <param name="innerException">Optional, exception before this one</param>
    public ApiExceptionInternalServerError(string message, System.Exception innerException = null) : base(
        HttpStatusCode.InternalServerError, message, innerException)
    {
    }

    /// <summary>
    ///     This method generates a example instance of this type
    /// </summary>
    /// <returns>The example instance of this type</returns>
    public override ApiExceptionModel GetExamples() =>
        GetExamples(HttpStatusCode.InternalServerError, "500 - Internal Server Error");
}
