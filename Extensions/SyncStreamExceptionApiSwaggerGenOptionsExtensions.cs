using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;
using SyncStream.Exception.Api.Filter;

// Define our namespace
namespace SyncStream.Exception.Api.Extensions;

/// <summary>
///     This class maintains our SwaggerGenOptions extensions
/// </summary>
public static class SyncStreamExceptionApiSwaggerGenOptionsExtensions
{
    /// <summary>
    ///     This method registers the SyncStream API Exception filters for Swagger/ReDoc
    /// </summary>
    /// <param name="instance">The current instance of SwaggerGenOptions</param>
    /// <returns>The current <paramref name="instance" /> of SwaggerGenOptions</returns>
    public static SwaggerGenOptions UseSyncStreamApiExceptionFilters(this SwaggerGenOptions instance)
    {
        // Add the operation filter
        instance.OperationFilter<ApiExceptionOperationFilter>();

        // We're done, return the instance
        return instance;
    }
}
