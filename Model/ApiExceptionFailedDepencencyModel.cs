using System.Net;
using Swashbuckle.AspNetCore.Filters;

// Define our namespace
namespace SyncStream.Exception.Api.Model;

/// <summary>
///     This class maintains our model structure for a 424 - Failed Dependency API exception
/// </summary>
public class ApiExceptionFailedDependencyModel : ApiExceptionModel, IExamplesProvider<ApiExceptionFailedDependencyModel>
{
    /// <summary>
    ///     This method instantiates an empty 424 - Failed Dependency API exception model
    /// </summary>
    public ApiExceptionFailedDependencyModel() : base(HttpStatusCode.FailedDependency)
    {
    }

    /// <summary>
    ///     This method instantiates a 424 - Failed Dependency exception model from an existing <paramref name="exception" />
    /// </summary>
    /// <param name="exception">The exception to generate the model from</param>
    public ApiExceptionFailedDependencyModel(System.Exception exception) :
        base(exception, HttpStatusCode.FailedDependency) => InnerException = exception.InnerException is not null
        ? new ApiExceptionFailedDependencyModel(exception.InnerException)
        : null;

    /// <summary>
    ///     This method generates an example instance of this type
    /// </summary>
    /// <returns>The example instance of this type</returns>
    public ApiExceptionFailedDependencyModel GetExamples() => new(new System.Exception("Failed Dependency"));
}
