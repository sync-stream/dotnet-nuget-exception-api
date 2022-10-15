using System.Net;
using Swashbuckle.AspNetCore.Filters;

// Define our namespace
namespace SyncStream.Exception.Api.Exception;

/// <summary>
///     This class maintains the structure of our 405 - Method Not Allowed API error
/// </summary>
public class ApiExceptionMethodNotAllowed : ApiException, IExamplesProvider<ApiExceptionMethodNotAllowed>
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
    public  ApiExceptionMethodNotAllowed() : base(HttpStatusCode.MethodNotAllowed) { }

    /// <summary>
    ///     This method generates a example instance of this type
    /// </summary>
    /// <returns>The example instance of this type</returns>
    public new ApiExceptionMethodNotAllowed GetExamples() =>
        GetExamples<ApiExceptionMethodNotAllowed>(HttpStatusCode.MethodNotAllowed, "405 - Method Not Allowed");
}
