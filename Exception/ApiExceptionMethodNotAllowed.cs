using System.Net;
using Swashbuckle.AspNetCore.Filters;
using SyncStream.Exception.Api.Model;

// Define our namespace
namespace SyncStream.Exception.Api.Exception;

/// <summary>
///     This class maintains the structure of our 405 - Method Not Allowed API error
/// </summary>
public class ApiExceptionMethodNotAllowed : ApiException, IExamplesProvider<ApiExceptionModel>
{
    /// <summary>
    ///     This method converts a system <paramref name="exception" /> to a 405 - Method Not Allowed API error
    /// </summary>
    /// <param name="exception">The system exception to convert</param>
    /// <returns></returns>
    public static ApiExceptionMethodNotAllowed FromSystemException(System.Exception exception) =>
        ApiException.FromSystemException<ApiExceptionMethodNotAllowed>(exception, HttpStatusCode.MethodNotAllowed);

    /// <summary>
    ///     This method instantiates an empty 405 - Method Not Allowed API error
    /// </summary>
    public ApiExceptionMethodNotAllowed() : base(HttpStatusCode.MethodNotAllowed)
    {
    }

    /// <summary>
    ///     This method instantiates a throwable 405 - Method Not Allowed API error with a <paramref name="message" /> and optional <paramref name="innerException" />
    /// </summary>
    /// <param name="message">The message describing what happened</param>
    /// <param name="innerException">Optional, exception before this one</param>
    public ApiExceptionMethodNotAllowed(string message, System.Exception innerException = null) : base(
        HttpStatusCode.MethodNotAllowed, message, innerException)
    {
    }

    /// <summary>
    ///     This method generates a example instance of this type
    /// </summary>
    /// <returns>The example instance of this type</returns>
    public override ApiExceptionModel GetExamples() =>
        GetExamples(HttpStatusCode.MethodNotAllowed, "Method Not Allowed");
}
