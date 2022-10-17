using System.Net;
using Swashbuckle.AspNetCore.Filters;

// Define our namespace
namespace SyncStream.Exception.Api.Model;

/// <summary>
///     This class maintains our model structure for a 409 - Conflict API exception
/// </summary>
public class ApiExceptionConflictModel : ApiExceptionModel, IExamplesProvider<ApiExceptionConflictModel>
{
    /// <summary>
    ///     This method instantiates an empty 409 - Conflict API exception model
    /// </summary>
    public ApiExceptionConflictModel() : base(HttpStatusCode.Conflict)
    {
    }

    /// <summary>
    ///     This method instantiates a 409 - Conflict exception model from an existing <paramref name="exception" />
    /// </summary>
    /// <param name="exception">The exception to generate the model from</param>
    public ApiExceptionConflictModel(System.Exception exception) : base(exception, HttpStatusCode.Conflict) =>
        InnerException = exception.InnerException is not null
            ? new ApiExceptionConflictModel(exception.InnerException)
            : null;

    /// <summary>
    ///     This method generates an example instance of this type
    /// </summary>
    /// <returns>The example instance of this type</returns>
    public ApiExceptionConflictModel GetExamples() => new(new System.Exception("Conflict"));
}
