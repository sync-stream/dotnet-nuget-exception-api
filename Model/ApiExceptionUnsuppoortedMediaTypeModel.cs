using System.Net;
using Swashbuckle.AspNetCore.Filters;

// Define our namespace
namespace SyncStream.Exception.Api.Model;

/// <summary>
///     This class maintains our model structure for a 415 - Unsupported Media Type API exception
/// </summary>
public class ApiExceptionUnsupportedMediaTypeModel : ApiExceptionModel,
    IExamplesProvider<ApiExceptionUnsupportedMediaTypeModel>
{
    /// <summary>
    ///     This method instantiates an empty 415 - Unsupported Media Type API exception model
    /// </summary>
    public ApiExceptionUnsupportedMediaTypeModel() : base(HttpStatusCode.UnsupportedMediaType)
    {
    }

    /// <summary>
    ///     This method instantiates a 415 - Unsupported Media Type exception model from an existing <paramref name="exception" />
    /// </summary>
    /// <param name="exception">The exception to generate the model from</param>
    public ApiExceptionUnsupportedMediaTypeModel(System.Exception exception) :
        base(exception, HttpStatusCode.UnsupportedMediaType) => InnerException = exception.InnerException is not null
        ? new ApiExceptionUnsupportedMediaTypeModel(exception.InnerException)
        : null;

    /// <summary>
    ///     This method generates an example instance of this type
    /// </summary>
    /// <returns>The example instance of this type</returns>
    public ApiExceptionUnsupportedMediaTypeModel GetExamples() => new(new System.Exception("Unsupported Media Type"));
}
