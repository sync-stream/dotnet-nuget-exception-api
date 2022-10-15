using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
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
    /// <param name="options">Optional, Swagger/ReDoc documentation generation options</param>
    /// <returns>The current <paramref name="instance" /> of IServiceCollection</returns>
    public static IServiceCollection UseSyncStreamExceptionDocumentation(this IServiceCollection instance,
        SwaggerGenOptions options = null)
    {
        // Add our operation filter to Swagger/ReDoc
        options?.OperationFilter<ApiExceptionOperationFilter>();

        // We're done, add the documentation examples to Swagger/ReDoc
        return instance.AddSwaggerExamplesFromAssemblyOf<ApiException>()
            .AddSwaggerExamplesFromAssemblyOf<ApiException>()
            .AddSwaggerExamplesFromAssemblyOf<ApiExceptionBadRequest>()
            .AddSwaggerExamplesFromAssemblyOf<ApiExceptionFailedDependency>()
            .AddSwaggerExamplesFromAssemblyOf<ApiExceptionInternalServerError>()
            .AddSwaggerExamplesFromAssemblyOf<ApiExceptionMethodNotAllowed>()
            .AddSwaggerExamplesFromAssemblyOf<ApiExceptionNotFound>()
            .AddSwaggerExamplesFromAssemblyOf<ApiExceptionUnauthorized>();
    }
}
