using System.Net;
using Swashbuckle.AspNetCore.Filters;

// Define our namespace
namespace SyncStream.Exception.Api.Exception;

/// <summary>
///     This class maintains the structure of our 404 - Not Found API error
/// </summary>
public class ApiExceptionNotFound : ApiException, IExamplesProvider<ApiExceptionNotFound>
{
    /// <summary>
    ///     This method converts a system <paramref name="exception" /> to a 404 - Not Found API error
    /// </summary>
    /// <param name="exception">The system exception to convert</param>
    /// <returns></returns>
    public static ApiExceptionNotFound FromSystemException(System.Exception exception) =>
        ApiException.FromSystemException<ApiExceptionNotFound>(exception, HttpStatusCode.NotFound);

    /// <summary>
    ///     This method instantiates an empty 404 - Not Found API error
    /// </summary>
    public  ApiExceptionNotFound() : base(HttpStatusCode.NotFound) { }

    /// <summary>
    ///     This method generates a example instance of this type
    /// </summary>
    /// <returns>The example instance of this type</returns>
    public new ApiExceptionNotFound GetExamples() =>
        GetExamples<ApiExceptionNotFound>(HttpStatusCode.NotFound, "404 - Not Found");
}
