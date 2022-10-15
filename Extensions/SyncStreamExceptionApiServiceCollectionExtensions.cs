using Microsoft.Extensions.DependencyInjection;
using SyncStream.Documentation.Extensions;
using SyncStream.Exception.Api.Exception;
using SyncStream.Exception.Api.Filter;

// Define our namespace
namespace SyncStream.Exception.Api.Extensions;

/// <summary>
///     This class maintains the structure of our IServiceCollection extensions
/// </summary>
public static class SyncStreamExceptionApiServiceCollectionExtensions
{
    /// <summary>
    ///     This method fluidly registers the API Exceptions and their documentation filters with Swagger/ReDoc
    /// </summary>
    /// <param name="instance">The current instance of IServiceCollection</param>
    /// <returns>The current <paramref name="instance" /> of IServiceCollection</returns>
    public static IServiceCollection UseSyncStreamExceptionDocumentation(this IServiceCollection instance) => instance
        .AddSyncStreamDocumentationOperationFilter<ApiExceptionOperationFilter>()
        .AddSyncStreamDocumentationExample<ApiException>()
        .AddSyncStreamDocumentationExample<ApiExceptionBadRequest>()
        .AddSyncStreamDocumentationExample<ApiExceptionFailedDependency>()
        .AddSyncStreamDocumentationExample<ApiExceptionInternalServerError>()
        .AddSyncStreamDocumentationExample<ApiExceptionMethodNotAllowed>()
        .AddSyncStreamDocumentationExample<ApiExceptionNotFound>()
        .AddSyncStreamDocumentationExample<ApiExceptionUnauthorized>();
}
