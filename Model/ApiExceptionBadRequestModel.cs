using System.Net;
using Swashbuckle.AspNetCore.Filters;

// Define our namespace
namespace SyncStream.Exception.Api.Model;

/// <summary>
///     This class maintains our model structure for a 400 - Bad Request API exception
/// </summary>
public class ApiExceptionBadRequestModel : ApiExceptionModel, IExamplesProvider<ApiExceptionBadRequestModel>
{
    /// <summary>
    ///     This method instantiates an empty 400 - Bad Request API exception model
    /// </summary>
    public ApiExceptionBadRequestModel() : base(HttpStatusCode.BadRequest)
    {
    }

    /// <summary>
    ///     This method instantiates a 400 - Bad Request exception model from an existing <paramref name="exception" />
    /// </summary>
    /// <param name="exception">The exception to generate the model from</param>
    public ApiExceptionBadRequestModel(System.Exception exception) : base(exception, HttpStatusCode.BadRequest) =>
        InnerException = exception.InnerException is not null
            ? new ApiExceptionBadRequestModel(exception.InnerException)
            : null;

    /// <summary>
    ///     This method generates an example instance of this type
    /// </summary>
    /// <returns>The example instance of this type</returns>
    public ApiExceptionBadRequestModel GetExamples() => new(new System.Exception("Bad Request"));
}
