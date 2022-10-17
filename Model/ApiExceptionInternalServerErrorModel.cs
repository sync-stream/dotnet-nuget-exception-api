using System.Net;
using Swashbuckle.AspNetCore.Filters;

// Define our namespace
namespace SyncStream.Exception.Api.Model;

/// <summary>
///     This class maintains our model structure for a 500 - Internal Server Error API exception
/// </summary>
public class ApiExceptionInternalServerErrorModel : ApiExceptionModel,
    IExamplesProvider<ApiExceptionInternalServerErrorModel>
{
    /// <summary>
    ///     This method instantiates an empty 500 - Internal Server Error API exception model
    /// </summary>
    public ApiExceptionInternalServerErrorModel() : base(HttpStatusCode.InternalServerError)
    {
    }

    /// <summary>
    ///     This method instantiates a 500 - Internal Server Error exception model from an existing <paramref name="exception" />
    /// </summary>
    /// <param name="exception">The exception to generate the model from</param>
    public ApiExceptionInternalServerErrorModel(System.Exception exception) : base(exception) => InnerException =
        exception.InnerException is not null
            ? new ApiExceptionInternalServerErrorModel(exception.InnerException)
            : null;

    /// <summary>
    ///     This method generates an example instance of this type
    /// </summary>
    /// <returns>The example instance of this type</returns>
    public ApiExceptionInternalServerErrorModel GetExamples() => new(new System.Exception("Internal Server Error"));
}
