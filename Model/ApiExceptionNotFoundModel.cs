using System.Net;
using Swashbuckle.AspNetCore.Filters;

// Define our namespace
namespace SyncStream.Exception.Api.Model;

/// <summary>
///     This class maintains our model structure for a 404 - Not Found API exception
/// </summary>
public class ApiExceptionNotFoundModel : ApiExceptionModel, IExamplesProvider<ApiExceptionNotFoundModel>
{
    /// <summary>
    ///     This method instantiates an empty 404 - Not Found API exception model
    /// </summary>
    public ApiExceptionNotFoundModel() : base(HttpStatusCode.NotFound)
    {
    }

    /// <summary>
    ///     This method instantiates a 404 - Not Found exception model from an existing <paramref name="exception" />
    /// </summary>
    /// <param name="exception">The exception to generate the model from</param>
    public ApiExceptionNotFoundModel(System.Exception exception) : base(exception, HttpStatusCode.NotFound) =>
        InnerException = exception.InnerException is not null
            ? new ApiExceptionNotFoundModel(exception.InnerException)
            : null;

    /// <summary>
    ///     This method generates an example instance of this type
    /// </summary>
    /// <returns>The example instance of this type</returns>
    public ApiExceptionNotFoundModel GetExamples() => new(new System.Exception("Not Found"));
}
