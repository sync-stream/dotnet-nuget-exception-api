using System.Net;
using SyncStream.Exception.Api.Exception;
using SyncStream.Exception.Api.Model;

// Define our namespace
namespace SyncStream.Exception.Api.Extensions;

/// <summary>
///     This class maintains our System.Exception extensions
/// </summary>
public static class SyncStreamExceptionApiSystemExceptionExtensions
{
    /// <summary>
    ///     This method converts the current System.Exception <paramref name="instance" /> into
    ///     a SyncStream ApiException with optional HTTP <paramref name="status" />
    /// </summary>
    /// <param name="instance">The current System.Exception instance</param>
    /// <param name="status">Optional, HTTP status to send to the client</param>
    /// <returns>The new ApiException instance</returns>
    public static ApiException ToSyncStreamApiException(this System.Exception instance,
        HttpStatusCode status = HttpStatusCode.InternalServerError) => status switch
    {
        // Add our 400 - Bad Request
        HttpStatusCode.BadRequest => new ApiExceptionBadRequest(instance),

        // Add our 424 - Failed Dependency
        HttpStatusCode.FailedDependency => new ApiExceptionFailedDependency(instance),

        // Add our 405 - Method Not Allowed
        HttpStatusCode.MethodNotAllowed => new ApiExceptionMethodNotAllowed(instance),

        // Add our 406 - Not Acceptable
        HttpStatusCode.NotAcceptable => new ApiExceptionNotAcceptable(instance),

        // Add our 404 - Not Found
        HttpStatusCode.NotFound => new ApiExceptionNotFound(instance),

        // Add our 501 - Not Implemented
        HttpStatusCode.NotImplemented => new ApiExceptionNotImplemented(instance),

        // Add our 401 - Unauthorized
        HttpStatusCode.Unauthorized => new ApiExceptionUnauthorized(instance),

        // Add our 415 - Unsupported Media Type
        HttpStatusCode.UnsupportedMediaType => new ApiExceptionUnsupportedMediaType(instance),

        // By default, send a 500 - Internal Server Error
        _ => new ApiExceptionInternalServerError(instance)
    };

    /// <summary>
    ///     This method coverts the current System.Exception <paramref name="instance" /> into
    ///     a SyncStream ApiExceptionModel with optional HTTP <paramref name="status" />
    /// </summary>
    /// <param name="instance">Te current System.Exception instance</param>
    /// <param name="status">Optional, HTTP status to send to the client</param>
    /// <returns>The new ApiExceptionModel instance</returns>
    public static ApiExceptionModel ToSyncStreamApiExceptionModel(this System.Exception instance,
        HttpStatusCode status = HttpStatusCode.InternalServerError) => status switch
    {
        // Add our 400 - Bad Request
        HttpStatusCode.BadRequest => new ApiExceptionBadRequestModel(instance),

        // Add our 424 - Failed Dependency
        HttpStatusCode.FailedDependency => new ApiExceptionFailedDependencyModel(instance),

        // Add our 405 - Method Not Allowed
        HttpStatusCode.MethodNotAllowed => new ApiExceptionMethodNotAllowedModel(instance),

        // Add our 406 - Not Acceptable
        HttpStatusCode.NotAcceptable => new ApiExceptionNotAcceptableModel(instance),

        // Add ur 404 - Not Found
        HttpStatusCode.NotFound => new ApiExceptionNotFoundModel(instance),

        // Add our 501 - Not Implemented
        HttpStatusCode.NotImplemented => new ApiExceptionNotImplementedModel(instance),

        // Add our 401 - Unauthorized
        HttpStatusCode.Unauthorized => new ApiExceptionUnauthorizedModel(instance),

        // Add our 415 - Unsupported Media Type
        HttpStatusCode.UnsupportedMediaType => new ApiExceptionUnsupportedMediaTypeModel(instance),

        // Byd default, send a 500 - Internal Server Error
        _ => new ApiExceptionInternalServerErrorModel(instance)
    };
}
