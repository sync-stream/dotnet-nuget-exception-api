using System.Net.Mime;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using SyncStream.Exception.Api.Exception;
using SyncStream.Serializer;

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
    /// <param name="suppressTracing">Optional, denotes whether the trace list should be cleared before writing the response</param>
    /// <returns>The current <paramref name="instance" /> of IApplicationBuilder</returns>
    public static IApplicationBuilder UseSyncStreamExceptionHandler(this IApplicationBuilder instance,
        bool suppressTracing = false) => instance.UseExceptionHandler(a => a.Run(async context =>
    {
        // Localize the exception handler feature from the application context
        IExceptionHandlerPathFeature exceptionFeature = context.Features.Get<IExceptionHandlerPathFeature>();

        // Localize the exception
        System.Exception exception = exceptionFeature?.Error ?? new("An Unknown Error Occurred");

        // Define our response
        ApiException response = ApiException.FromSystemException(exception);

        // Define our XML media types
        List<string> xmlMediaTypes = new() {MediaTypeNames.Application.Xml, MediaTypeNames.Application.Json};

        // Localize the acceptance header
        string contentType = context.Request.Headers.Where(h => h.Key.ToLower().Equals("accept"))
            .Select(h => h.Value.ToString()).FirstOrDefault();

        // Make sure we have a content type
        if (string.IsNullOrEmpty(contentType) || string.IsNullOrWhiteSpace(contentType))
            contentType = context.Request.ContentType?.ToLower() ?? MediaTypeNames.Application.Json;

        // Check for production and clear the traces
        if (suppressTracing) response?.Trace.Clear();

        // Define our serialized response
        string serializedResponse = SerializerService.Serialize(response,
            xmlMediaTypes.Contains(contentType) ? SerializerFormat.Xml : SerializerFormat.Json);

        // Set the content length into the response
        context.Response.ContentLength = serializedResponse.Length;

        // Send the content-type header
        context.Response.ContentType = xmlMediaTypes.Contains(contentType)
            ? MediaTypeNames.Application.Xml
            : MediaTypeNames.Application.Json;

        // Set the status code into the response
        context.Response.StatusCode = response?.Code ?? 500;

        // We're done, write the response
        await context.Response.WriteAsync(serializedResponse);
    }));
}
