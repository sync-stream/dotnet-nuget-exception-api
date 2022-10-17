using System.Net;
using Swashbuckle.AspNetCore.Filters;
using SyncStream.Exception.Api.Exception;

// Define our namespace
namespace SyncStream.Exception.Api.Model;

/// <summary>
///     This class maintains our model structure for a 406 - Not Acceptable API exception
/// </summary>
public class ApiExceptionNotAcceptableModel : ApiExceptionModel, IExamplesProvider<ApiExceptionNotAcceptableModel>
{
    /// <summary>
    ///     This method instantiates an empty 406 - Not Acceptable API exception model
    /// </summary>
    public ApiExceptionNotAcceptableModel() : base(HttpStatusCode.NotAcceptable)
    {
    }

    /// <summary>
    ///     This method instantiates a 406 - Not Acceptable exception model from an existing <paramref name="exception" />
    /// </summary>
    /// <param name="exception">The exception to generate the model from</param>
    public ApiExceptionNotAcceptableModel(System.Exception exception) : base(exception, HttpStatusCode.NotAcceptable) =>
        InnerException = exception.InnerException is not null
            ? new ApiExceptionNotFoundModel(exception.InnerException)
            : null;

    /// <summary>
    ///     This method generates an example instance of this type
    /// </summary>
    /// <returns>The example instance of this type</returns>
    public ApiExceptionNotAcceptableModel GetExamples() => new(new System.Exception("Not Acceptable"));
}
