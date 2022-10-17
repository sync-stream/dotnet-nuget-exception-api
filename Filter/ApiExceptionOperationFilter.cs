using System.Net;
using System.Net.Mime;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using SyncStream.Exception.Api.Model;
using SyncStream.Serializer;

// Define our namespace
namespace SyncStream.Exception.Api.Filter;

/// <summary>
///     This class maintains our XML example serializer filter for Swagger
/// </summary>
public class ApiExceptionOperationFilter : IOperationFilter
{
    /// <summary>
    ///     This property contains our exceptions
    /// </summary>
    private static List<ApiExceptionModel> ExceptionModels => new()
    {
        // Add our 400 - Bad Request
        new ApiExceptionBadRequestModel().GetExamples(),

        // Add our 401 - Unauthorized
        new ApiExceptionUnauthorizedModel().GetExamples(),

        // Add our 404 - Not found
        new ApiExceptionNotFoundModel().GetExamples(),

        // Add our 405 - Method Not Allowed
        new ApiExceptionMethodNotAllowedModel().GetExamples(),

        // Add our 406 - Not Acceptable
        new ApiExceptionNotAcceptableModel().GetExamples(),

        // Add our 415 - Unsupported Media Type
        new ApiExceptionUnsupportedMediaTypeModel().GetExamples(),

        // Add our 424 - Dependency Failed
        new ApiExceptionFailedDependencyModel().GetExamples(),

        // Add our 500 - Internal Server Error
        new ApiExceptionInternalServerErrorModel().GetExamples(),

        // Add our 501 - Not Implemented
        new ApiExceptionNotImplementedModel().GetExamples()
    };

    /// <summary>
    ///     This method generates XML examples using our serializer
    /// </summary>
    /// <param name="operation">The current OpenApi operation</param>
    /// <param name="context">The current OpenApi filter context</param>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // Iterate over the API exceptions
        foreach (ApiExceptionModel model in ExceptionModels)
        {
            // Check the include-unauthorized flag and add the error
            if (model.Status is HttpStatusCode.Unauthorized &&
                (context.MethodInfo.GetCustomAttribute<AuthorizeAttribute>(false) is null ||
                 context.MethodInfo.DeclaringType?.GetCustomAttribute<AuthorizeAttribute>(false) is null)) continue;

            // Try to lookup our schema and generate one if it doesn't exist
            if (!context.SchemaRepository.TryLookupByType(model.GetType(), out OpenApiSchema schema))
                schema = context.SchemaGenerator.GenerateSchema(model.GetType(), context.SchemaRepository);

            // Define our response
            OpenApiResponse response = new OpenApiResponse
            {
                // Set the description into the response
                Description = $"**{model.GetType().Name}** - {model.Status.ToString()} - {model.Message}"
            };

            // Iterate over the content items in the response
            foreach (KeyValuePair<string, OpenApiMediaType> content in operation.Responses.First().Value.Content)
                response.Content[content.Key] = new()
                {
                    // Set the example into the schema
                    Example = new OpenApiString(
                        SerializerService.SerializePretty(model,
                            content.Key.ToLower() is MediaTypeNames.Application.Xml
                                ? SerializerFormat.Xml
                                : SerializerFormat.Json), true, content.Key is MediaTypeNames.Application.Json),

                    // Set the schema into the media type
                    Schema = schema
                };

            // Add the response to the operation
            operation.Responses[model.Code.ToString()] = response;
        }
    }
}
