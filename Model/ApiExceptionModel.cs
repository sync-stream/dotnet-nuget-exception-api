using System.Net;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Diagnostics;
using SyncStream.Exception.Api.Exception;
using SyncStream.Exception.Api.Extensions;
using SyncStream.Serializer.Model;

// Define our namespace
namespace SyncStream.Exception.Api.Model;

/// <summary>
///     This class maintains the API model structure for our exceptions
/// </summary>
[XmlInclude(typeof(SerializableKeyValuePairModel<string, object>))]
[XmlInclude(typeof(ApiExceptionTraceModel))]
[XmlInclude(typeof(ApiExceptionBadRequestModel))]
[XmlInclude(typeof(ApiExceptionConflictModel))]
[XmlInclude(typeof(ApiExceptionFailedDependencyModel))]
[XmlInclude(typeof(ApiExceptionInternalServerErrorModel))]
[XmlInclude(typeof(ApiExceptionMethodNotAllowedModel))]
[XmlInclude(typeof(ApiExceptionNotAcceptableModel))]
[XmlInclude(typeof(ApiExceptionNotFoundModel))]
[XmlInclude(typeof(ApiExceptionNotImplementedModel))]
[XmlInclude(typeof(ApiExceptionRequestTimeoutModel))]
[XmlInclude(typeof(ApiExceptionUnauthorizedModel))]
[XmlInclude(typeof(ApiExceptionUnsupportedMediaTypeModel))]
[XmlRoot("exception")]
public abstract class ApiExceptionModel : IExceptionHandlerFeature
{
    /// <summary>
    ///     This property contains the numeric value of the HTTP status code
    /// </summary>
    [JsonPropertyName("code")]
    [XmlAttribute("code")]
    public int Code { get; set; } = 500;

    /// <summary>
    ///     This property contains the data associated with the exception
    /// </summary>
    [JsonPropertyName("data")]
    [XmlIgnore]
    public Dictionary<string, object> Data { get; set; } = new();

    /// <summary>
    ///     This property contains the original exception the model is generated from
    /// </summary>
    [JsonIgnore]
    [XmlIgnore]
    public System.Exception Error { get; }

    /// <summary>
    ///     This property contains the xml-serializable data associated with the exception
    /// </summary>
    [JsonIgnore]
    [XmlElement("data")]
    public List<SerializableKeyValuePairModel<string, object>> DataXml
    {
        get => Data is not null
            ? SerializableKeyValuePairModel<string, object>.FromDictionary(Data)
            : null;
        set => Data = value is not null
            ? new Dictionary<string, object>(value.Select(v => v.ToKeyValuePair()))
            : null;
    }

    /// <summary>
    ///     This property contains the inner exception, if one occurred
    /// </summary>
    [JsonPropertyName("innerException")]
    [XmlElement("innerExceptoin")]
    public ApiExceptionModel InnerException { get; set; }

    /// <summary>
    ///     This property contains the textual message describing what happened
    /// </summary>
    [JsonPropertyName("message")]
    [XmlElement("message")]
    public string Message { get; set; }

    /// <summary>
    ///     This property contains the HTTP status code
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    [JsonPropertyName("status")]
    [XmlAttribute("status")]
    public HttpStatusCode Status { get; set; } = HttpStatusCode.InternalServerError;

    /// <summary>
    ///     This property contains the exception trace, if there is one
    /// </summary>
    [JsonPropertyName("trace")]
    [XmlElement("trace")]
    public List<ApiExceptionTraceModel> Trace { get; set; } = new();

    /// <summary>
    ///     This method instantiates an empty model
    /// </summary>
    public ApiExceptionModel()
    {
    }

    /// <summary>
    ///     This method instantiates a model with an HTTP <paramref name="status" />
    /// </summary>
    /// <param name="status">The HTTP status to send to the client</param>
    public ApiExceptionModel(HttpStatusCode status)
    {
        // Set the HTTP status code into the instance
        Code = (int) status;

        // Set the HTTP status into the instance
        Status = status;
    }

    /// <summary>
    ///     This method instantiates a model from an existing <paramref name="exception" /> with optional HTTP <paramref name="status" />
    /// </summary>
    /// <param name="exception">The exception to generate the model from</param>
    /// <param name="status">Optional, HTTP status to send to the client</param>
    protected ApiExceptionModel(System.Exception exception, HttpStatusCode status = HttpStatusCode.InternalServerError)
    {
        // Set the HTTP status code into the instance
        Code = exception.GetType().IsSubclassOf(typeof(ApiException))
            ? (exception as ApiException)?.Code ?? (int) status
            : (int) status;

        // Set the data into the instance
        Data = exception.Data as Dictionary<string, object>;

        // Set the error into the instance
        Error = exception;

        // Set the inner exception into the instance
        InnerException = exception.InnerException?.ToSyncStreamApiExceptionModel(status);

        // Set the message into the instance
        Message = exception.Message;

        // Set the HTTP status into the instance
        Status = exception.GetType().IsSubclassOf(typeof(ApiException))
            ? (exception as ApiException)?.Status ?? status
            : status;

        // Set the trace into the instance
        Trace = exception.StackTrace?.Split("\n", StringSplitOptions.TrimEntries)
            .Select(t => new ApiExceptionTraceModel(t.Trim())).Where(t => t.IsValid()).ToList();
    }
}
