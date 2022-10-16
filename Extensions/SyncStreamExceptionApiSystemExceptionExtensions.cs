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
        HttpStatusCode status = HttpStatusCode.InternalServerError) =>
        ApiException.FromSystemException(instance, status);

    /// <summary>
    ///     This method coverts the current System.Exception <paramref name="instance" /> into
    ///     a SyncStream ApiExceptionModel with optional HTTP <paramref name="status" />
    /// </summary>
    /// <param name="instance">Te current System.Exception instance</param>
    /// <param name="status">Optional, HTTP status to send to the client</param>
    /// <returns>The new ApiExceptionModel instance</returns>
    public static ApiExceptionModel ToSyncStreamApiExceptionModel(this System.Exception instance,
        HttpStatusCode status = HttpStatusCode.InternalServerError) =>
        instance.ToSyncStreamApiException(status).ToModel();
}
