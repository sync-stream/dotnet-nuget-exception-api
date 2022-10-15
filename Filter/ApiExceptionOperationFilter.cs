using System.Net.Mime;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using SyncStream.Exception.Api.Exception;
using SyncStream.Serializer;

// Define our namespace
namespace SyncStream.Exception.Api.Filter;

/// <summary>
/// This class maintains our XML example serializer filter for Swagger
/// </summary>
public class ApiExceptionOperationFilter : IOperationFilter
{
    /// <summary>
    /// This method generates XML examples using our serializer
    /// </summary>
    /// <param name="operation"></param>
    /// <param name="context"></param>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // Define our errors, starting with the 400 - Bad Request
        List<ApiException> responseErrors = new()
            {ApiExceptionBadRequest.FromSystemException(new("400 - Bad Request"))};

        // Check the include-unauthorized flag and add the error
        if (context.MethodInfo.GetCustomAttribute<AuthorizeAttribute>(false) != null ||
            context.MethodInfo.DeclaringType?.GetCustomAttribute<AuthorizeAttribute>(false) != null &&
            context.MethodInfo.GetCustomAttribute<AllowAnonymousAttribute>(false) == null)
            responseErrors.Add(ApiExceptionUnauthorized.FromSystemException(new("401 - Unauthorized")));

        // Add our 404 - Not found
        responseErrors.Add(ApiExceptionNotFound.FromSystemException(new("404 - Not Found")));

        // Add our 405 - Method Not Allowed
        responseErrors.Add(ApiExceptionMethodNotAllowed.FromSystemException(new("405 -  Method Not Allowed")));

        // Add our 424 - Dependency Failed
        responseErrors.Add(ApiExceptionFailedDependency.FromSystemException(new("424 -  Failed Dependency")));

        // Add our 500 - Internal Server Error
        responseErrors.Add(ApiExceptionInternalServerError.FromSystemException(new("500 - Internal Server Error")));

        // Iterate over the operation responses
        foreach (ApiException responseError in responseErrors)
        {
            // Try to lookup our schema and generate one if it doesn't exist
            if (!context.SchemaRepository.TryLookupByType(responseError.GetType(), out OpenApiSchema schema))
                schema = context.SchemaGenerator.GenerateSchema(responseError.GetType(), context.SchemaRepository);

            // Define our response
            OpenApiResponse response = new OpenApiResponse();

            // Set the description into the response
            response.Description =
                $"**{schema.Title ?? responseError.GetType().Name}** - {responseError.Status.ToString()} - {schema.Description ?? responseError.Message}";

            // Iterate over the content items in the response
            foreach (KeyValuePair<string, OpenApiMediaType> content in operation.Responses.First().Value.Content)
            {
                // Define our media type response
                OpenApiMediaType mediaType = new OpenApiMediaType();

                // Check the content-type for XML and generate an example
                if (content.Key.ToLower().Equals(MediaTypeNames.Application.Xml.ToLower()))
                    mediaType.Example =
                        new OpenApiString(SerializerService.SerializePretty(responseError.GetExamples(),
                            SerializerFormat.Xml));

                // Set the schema into the media type
                mediaType.Schema = schema;

                // Add the content to the response
                response.Content[content.Key] = mediaType;
            }

            // Add the response to the operation
            operation.Responses[responseError.Code.ToString()] = response;
        }
    }
}
