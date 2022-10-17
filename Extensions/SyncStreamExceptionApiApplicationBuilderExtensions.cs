using System.Net;
using Microsoft.AspNetCore.Builder;
using SyncStream.Exception.Api.Middleware;

// Define our namespace
namespace SyncStream.Exception.Api.Extensions;

/// <summary>
///     This class maintains our API Exception extensions for IApplicationBuilder
/// </summary>
public static class SyncStreamExceptionApiApplicationBuilderExtensions
{
    /// <summary>
    ///     This method registers the SyncStream Global Exception handler that catches and
    ///     serialized exceptions without the need for explicit try/catch statements
    /// </summary>
    /// <param name="instance">The current instance of IApplicationBuilder</param>
    /// <param name="defaultStatus">Optional, default HTTP status code to send to the client for non-API exceptions</param>
    /// <param name="suppressTracing">Optional, denotes whether the trace list should be cleared before writing the response</param>
    /// <returns>The current <paramref name="instance" /> of IApplicationBuilder</returns>
    public static IApplicationBuilder UseSyncStreamExceptionHandler(this IApplicationBuilder instance,
        HttpStatusCode defaultStatus = HttpStatusCode.InternalServerError, bool suppressTracing = false)
    {
        // Set the default HTTP status code to send to the client
        ApiExceptionHandlerMiddleware.DefaultHttpStatusCode = defaultStatus;

        // Set the trace suppressing flag
        ApiExceptionHandlerMiddleware.SuppressTracing = suppressTracing;

        // We're done, register our exception handling middleware
        return instance.UseMiddleware<ApiExceptionHandlerMiddleware>();
    }
}
