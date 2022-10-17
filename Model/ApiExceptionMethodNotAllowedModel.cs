using System.Net;
using Swashbuckle.AspNetCore.Filters;

// Define our namespace
namespace SyncStream.Exception.Api.Model;

/// <summary>
///     This class maintains our model structure for a 405 - Method Not Allowed API exception
/// </summary>
public class ApiExceptionMethodNotAllowedModel : ApiExceptionModel, IExamplesProvider<ApiExceptionMethodNotAllowedModel>
{
    /// <summary>
    ///     This method instantiates an empty 405 - Method Not Allowed API exception model
    /// </summary>
    public ApiExceptionMethodNotAllowedModel() : base(HttpStatusCode.MethodNotAllowed)
    {
    }

    /// <summary>
    ///     This method instantiates a 405 - Method Not Allowed exception model from an existing <paramref name="exception" />
    /// </summary>
    /// <param name="exception">The exception to generate the model from</param>
    public ApiExceptionMethodNotAllowedModel(System.Exception exception) :
        base(exception, HttpStatusCode.MethodNotAllowed) => InnerException = exception.InnerException is not null
        ? new ApiExceptionMethodNotAllowedModel(exception.InnerException)
        : null;

    /// <summary>
    ///     This method generates an example instance of this type
    /// </summary>
    /// <returns>The example instance of this type</returns>
    public ApiExceptionMethodNotAllowedModel GetExamples() => new(new System.Exception("Method Not Allowed"));
}
