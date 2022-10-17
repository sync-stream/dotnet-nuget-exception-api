using System.Net;
using Swashbuckle.AspNetCore.Filters;

// Define our namespace
namespace SyncStream.Exception.Api.Model;

/// <summary>
///     This class maintains our model structure for a 408 - Request Timeout API exception
/// </summary>
public class ApiExceptionRequestTimeoutModel : ApiExceptionModel, IExamplesProvider<ApiExceptionRequestTimeoutModel>
{
    /// <summary>
    ///     This method instantiates an empty 408 - Request Timeout API exception model
    /// </summary>
    public ApiExceptionRequestTimeoutModel() : base(HttpStatusCode.RequestTimeout)
    {
    }

    /// <summary>
    ///     This method instantiates a 408 - Request Timeout exception model from an existing <paramref name="exception" />
    /// </summary>
    /// <param name="exception">The exception to generate the model from</param>
    public ApiExceptionRequestTimeoutModel(System.Exception exception) :
        base(exception, HttpStatusCode.RequestTimeout) => InnerException = exception.InnerException is not null
        ? new ApiExceptionRequestTimeoutModel(exception.InnerException)
        : null;

    /// <summary>
    ///     This method generates an example instance of this type
    /// </summary>
    /// <returns>The example instance of this type</returns>
    public ApiExceptionRequestTimeoutModel GetExamples() => new(new System.Exception("Request Timeout"));
}
