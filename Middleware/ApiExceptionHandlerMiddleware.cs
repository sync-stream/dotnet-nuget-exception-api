using System.Net;
using System.Net.Mime;
using System.Runtime.ExceptionServices;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;
using SyncStream.Exception.Api.Extensions;
using SyncStream.Exception.Api.Model;
using SyncStream.Serializer;

// Define our namespace
namespace SyncStream.Exception.Api.Middleware;

/// <summary>
///     A middleware for handling exceptions in the application
/// </summary>
internal class ApiExceptionHandlerMiddleware
{
    /// <summary>
    ///     This constant defines the default HTTP status code
    /// </summary>
    public static HttpStatusCode DefaultHttpStatusCode { get; set; } = HttpStatusCode.InternalServerError;

    /// <summary>
    ///     This property denotes whether to clear traces before sending them to the client or not
    /// </summary>
    public static bool SuppressTracing { get; set; } = false;

    /// <summary>
    ///     This method awaits a call-stack action
    /// </summary>
    /// <param name="middleware">The middleware responsible for handling any exceptions</param>
    /// <param name="context">The current HTTP context</param>
    /// <param name="task">The task to be awaited</param>
    private static async Task Awaited(ApiExceptionHandlerMiddleware middleware, HttpContext context, Task task)
    {
        // Define our exception dispatch information
        ExceptionDispatchInfo exceptionDispatchInfo = null;

        // Try to await the task
        try
        {
            // Await the task
            await task;
        }

        // Catch any exceptions
        catch (System.Exception exception)
        {
            // Get the Exception, but don't continue processing in the catch block as its bad for stack usage.
            exceptionDispatchInfo = ExceptionDispatchInfo.Capture(exception);
        }

        // Ensure we have an exception dispatch info and handle the exception
        if (exceptionDispatchInfo is not null) await middleware.HandleException(context, exceptionDispatchInfo);
    }

    /// <summary>
    ///     This method clears the cache headers from an object <paramref name="state" />
    /// </summary>
    /// <param name="state">The HTTP response to clear cache headers from</param>
    private static Task ClearCacheHeaders(object state)
    {
        // Localize the headers from the HTTP response
        IHeaderDictionary headers = ((HttpResponse) state).Headers;

        // Set the e-tag
        headers.ETag = default;

        // Set the expiration date
        headers.Expires = "-1";

        // Set the cache-control
        headers.CacheControl = "no-cache,no-store";

        // Set the pragma
        headers.Pragma = "no-cache";

        // We're done, send a completed task
        return Task.CompletedTask;
    }

    /// <summary>
    ///     This method clears an HTTP <paramref name="context" />
    /// </summary>
    /// <param name="context"></param>
    private static void ClearHttpContext(HttpContext context)
    {
        // Clear the response
        context.Response.Clear();

        // An endpoint may have already been set. Since we're going to re-invoke the middleware pipeline we need to reset
        // the endpoint and route values to ensure things are re-calculated.
        context.SetEndpoint(endpoint: null);

        // Localize the route values feature
        IRouteValuesFeature routeValuesFeature = context.Features.Get<IRouteValuesFeature>();

        // Check to see if we have any route values features
        if (routeValuesFeature is not null) routeValuesFeature.RouteValues = null!;
    }

    /// <summary>
    ///     This property contains the delegate responsible for clearing cache headers
    /// </summary>
    private readonly Func<object, Task> _clearCacheHeadersDelegate;

    /// <summary>
    ///     This property contains the instance of our log service provider
    /// </summary>
    private readonly ILogger _logger;

    /// <summary>
    ///     This property contains the delegate for the next action in the call-stack
    /// </summary>
    private readonly RequestDelegate _next;

    /// <summary>
    ///     This method instantiates our exception handler with injected dependencies
    /// </summary>
    /// <param name="next">The <see cref="RequestDelegate"/> representing the next middleware in the pipeline.</param>
    /// <param name="loggerFactory">The <see cref="ILoggerFactory"/> used for logging.</param>
    public ApiExceptionHandlerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
    {
        // Set the header-cache clearing delegate
        _clearCacheHeadersDelegate = ClearCacheHeaders;

        // Set the log service provider into the instance
        _logger = loggerFactory.CreateLogger<ApiExceptionHandlerMiddleware>();

        // Set the delegate for the next action in the call-stack for the instance
        _next = next;
    }

    /// <summary>
    ///     This method invokes the middleware
    /// </summary>
    /// <param name="context">The <see cref="HttpContext"/> for the current request.</param>
    public Task Invoke(HttpContext context)
    {
        // Define our exception dispatch information
        ExceptionDispatchInfo ediExceptionDispatchInfo;

        // Try to localize and execute the next action in the call-stack
        try
        {
            // Localize te next action in the call-stack
            Task task = _next(context);

            // Check for a successfully completed task
            if (!task.IsCompletedSuccessfully) return Awaited(this, context, task);

            // We're done, return the completed task
            return Task.CompletedTask;
        }

        // Catch any exceptions
        catch (System.Exception exception)
        {
            // Get the Exception, but don't continue processing in the catch block as its bad for stack usage.
            ediExceptionDispatchInfo = ExceptionDispatchInfo.Capture(exception);
        }

        // We're done, handle the exception and send the response
        return HandleException(context, ediExceptionDispatchInfo);
    }

    /// <summary>
    ///     This method asynchronously handles an unhandled exception
    /// </summary>
    /// <param name="context">The HTTP context to send the response to</param>
    /// <param name="exceptionDispatchInfo">The exception information</param>
    private async Task HandleException(HttpContext context, ExceptionDispatchInfo exceptionDispatchInfo)
    {
        // Log the exception
        _logger.LogError(exceptionDispatchInfo.SourceException,
            "ApiExceptionHandlerMiddleware:\tHandlingUnhandledException");

        // We can't do anything if the response has already started, just abort.
        if (context.Response.HasStarted)
        {
            // Send the log message
            _logger.LogError("ApiExceptionHandlerMiddleware:\tResponseAlreadyStarted");

            // Rethrow the exception
            exceptionDispatchInfo.Throw();
        }

        // Try to handle the exception
        try
        {
            // Localize our model
            ApiExceptionModel model =
                exceptionDispatchInfo.SourceException.ToSyncStreamApiExceptionModel(DefaultHttpStatusCode);

            // Clear the HTTP context
            ClearHttpContext(context);

            // Clear the headers on response starting
            context.Response.OnStarting(_clearCacheHeadersDelegate, context.Response);

            // Set the HTTP status code into the response
            context.Response.StatusCode = model.Code;

            // Set the exception handling feature into the response
            context.Features.Set<IExceptionHandlerFeature>(model);

            // Localize the acceptance header
            string contentType = context.Request.Headers.Accept.ToString()?.ToLower() ??
                                 context.Request.Headers.ContentType.ToString()?.ToLower() ??
                                 MediaTypeNames.Application.Json;

            // Check for production and clear the traces
            if (SuppressTracing) model.Trace.Clear();

            // Define our serialized response
            string response = SerializerService.Serialize(model,
                contentType is MediaTypeNames.Application.Xml ? SerializerFormat.Xml : SerializerFormat.Json);

            // Set the content length into the response
            context.Response.ContentLength = response.Length;

            // Send the content-type header
            context.Response.ContentType = contentType;

            // Set the status code into the response
            context.Response.StatusCode = model.Code;

            // We're done, write the response
            await context.Response.WriteAsync(response);
        }

        // Catch any exceptions
        catch (System.Exception exception)
        {
            // Log the exception, but suppress it
            _logger.LogError(exception, "ApiExceptionHandlerMiddleware:\tMiddlewareException");
        }

        // Re-throw wrapped exception or the original if we couldn't handle it
        exceptionDispatchInfo.Throw();
    }
}
