using System.Net;
using Swashbuckle.AspNetCore.Filters;

// Define our namespace
namespace SyncStream.Exception.Api.Model;

/// <summary>
///     This class maintains our model structure for a 401 - Unauthorized API exception
/// </summary>
public class ApiExceptionUnauthorizedModel : ApiExceptionModel, IExamplesProvider<ApiExceptionUnauthorizedModel>
{
    /// <summary>
    ///     This method instantiates an empty 401 - Unauthorized API exception model
    /// </summary>
    public ApiExceptionUnauthorizedModel() : base(HttpStatusCode.Unauthorized)
    {
    }

    /// <summary>
    ///     This method instantiates a 401 - Unauthorized exception model from an existing <paramref name="exception" />
    /// </summary>
    /// <param name="exception">The exception to generate the model from</param>
    public ApiExceptionUnauthorizedModel(System.Exception exception) : base(exception, HttpStatusCode.Unauthorized) =>
        InnerException = exception.InnerException is not null
            ? new ApiExceptionUnauthorizedModel(exception.InnerException)
            : null;

    /// <summary>
    ///     This method generates an example instance of this type
    /// </summary>
    /// <returns>The example instance of this type</returns>
    public ApiExceptionUnauthorizedModel GetExamples() => new(new System.Exception("Unauthorized"));
}
