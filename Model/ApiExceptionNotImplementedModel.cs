using System.Net;
using Swashbuckle.AspNetCore.Filters;

// Define our namespace
namespace SyncStream.Exception.Api.Model;

/// <summary>
///     This class maintains our model structure for a 501 - Not Implemented API exception
/// </summary>
public class ApiExceptionNotImplementedModel : ApiExceptionModel, IExamplesProvider<ApiExceptionNotImplementedModel>
{
    /// <summary>
    ///     This method instantiates an empty 501 - Not Implemented API exception model
    /// </summary>
    public ApiExceptionNotImplementedModel() : base(HttpStatusCode.NotImplemented)
    {
    }

    /// <summary>
    ///     This method instantiates a 501 - Not Implemented exception model from an existing <paramref name="exception" />
    /// </summary>
    /// <param name="exception">The exception to generate the model from</param>
    public ApiExceptionNotImplementedModel(System.Exception exception) :
        base(exception, HttpStatusCode.NotImplemented) => InnerException = exception.InnerException is not null
        ? new ApiExceptionNotImplementedModel(exception.InnerException)
        : null;

    /// <summary>
    ///     This method generates an example instance of this type
    /// </summary>
    /// <returns>The example instance of this type</returns>
    public ApiExceptionNotImplementedModel GetExamples() => new(new System.Exception("Not Implemented"));
}
