using System.Diagnostics;
using System.Net;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using Swashbuckle.AspNetCore.Filters;
using SyncStream.Exception.Api.Model;

// Define our namespace
namespace SyncStream.Exception.Api.Exception;

/// <summary>
///     This class maintains the model structure for our API exception
/// </summary>
[XmlInclude(typeof(ApiExceptionTrace))]
[XmlInclude(typeof(List<ApiExceptionTrace>))]
[XmlRoot("exception")]
public class ApiException : System.Exception, IExamplesProvider<ApiException>
{
    /// <summary>
    ///     This method converts a system exception into an API exception
    /// </summary>
    /// <param name="exception">The system exception to convert</param>
    /// <param name="status">The HTTP status to assign to the API exception</param>
    /// <returns>The new API exception</returns>
    /// <typeparam name="TApiException">The final type expectation of the response</typeparam>
    public static TApiException FromSystemException<TApiException>(System.Exception exception,
        HttpStatusCode status = HttpStatusCode.InternalServerError) where TApiException : ApiException, new() => new()
    {
        // Set the HTTP status code into the response
        Code = (int) status,

        // Set the data into the response
        Data = exception.Data as Dictionary<string, object>,

        // Set the inner exception into the response
        InnerException = exception.InnerException is not null
            ? FromSystemException<TApiException>(exception.InnerException, status)
            : null,

        // Set the exception message into the response
        Message = exception.Message,

        // Set the HTTP status into the response
        Status = status,

        // Set the exception trace into the response
        Trace = !string.IsNullOrEmpty(exception.StackTrace) && !string.IsNullOrWhiteSpace(exception.StackTrace)
            ? exception.StackTrace.Split("\n").Select(t => new ApiExceptionTrace(t.Trim())).Where(t => t.IsValid())
                .ToList()
            : new()
    };

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
    public new Dictionary<string, object> Data { get; set; } = new();

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
    public new ApiException InnerException { get; set; }

    /// <summary>
    ///     This property contains the textual message describing what happened
    /// </summary>
    [JsonPropertyName("message")]
    [XmlElement("message")]
    public new string Message { get; set; }

    /// <summary>
    ///     This property contains the HTTP status code
    /// </summary>
    [JsonPropertyName("status")]
    [XmlAttribute("status")]
    public HttpStatusCode Status { get; set; } = HttpStatusCode.InternalServerError;

    /// <summary>
    ///     This property contains the internal stack trace string
    /// </summary>
    [JsonIgnore]
    [XmlIgnore]
    public sealed override string StackTrace => new StackTrace(this, fNeedFileInfo: true).ToString().Trim();

    /// <summary>
    ///     This property contains the exception trace, if there is one
    /// </summary>
    [JsonPropertyName("trace")]
    [XmlElement("trace")]
    public List<ApiExceptionTrace> Trace { get; set; } = new();

    /// <summary>
    ///     This method instantiates an empty throwable API exception
    /// </summary>
    public ApiException()
    {
    }

    /// <summary>
    ///     This method instantiates a throwable API exception with an HTTP <paramref name="status" />
    /// </summary>
    /// <param name="status">The HTTP status to send to the client</param>
    public ApiException(HttpStatusCode status)
    {
        // Set the HTTP status code into the instance
        Code = (int) status;

        // Set the HTTP status into the instance
        Status = status;
    }

    /// <summary>
    ///     This method instantiates a throwable API exception with a <paramref name="message" /> and optional <paramref name="innerException" />
    /// </summary>
    /// <param name="message">The message describing what happened</param>
    /// <param name="innerException">Optional, exception before this one</param>
    public ApiException(string message, System.Exception innerException = null) : base(message)
    {
        // Check for an inner exception and set it
        if (innerException is not null)
            InnerException = innerException.GetType().IsSubclassOf(typeof(ApiException))
                ? innerException as ApiException
                : FromSystemException<ApiException>(innerException);

        // Set the trace into the instance
        Trace = StackTrace?.Split("\n", StringSplitOptions.TrimEntries).Select(t => new ApiExceptionTrace(t.Trim()))
            .Where(t => t.IsValid()).ToList();
    }

    /// <summary>
    ///     This method instantiates a throwable API exception with an HTTP <paramref name="status" />, <paramref name="message" /> and optional <paramref name="innerException" />
    /// </summary>
    /// <param name="status">The HTTP status to send to the browser</param>
    /// <param name="message">The message describing what happened</param>
    /// <param name="innerException">Optional, exception before this one</param>
    public ApiException(HttpStatusCode status, string message, System.Exception innerException = null) : this(message,
        innerException)
    {
        // Set the HTTP status code into the instance
        Code = (int) status;

        // Set the HTTP status into the instance
        Status = status;
    }

    /// <summary>
    ///     This method generates an example instance of this type for child inheritance
    /// </summary>
    /// <param name="status">The HTTP status code for the error</param>
    /// <param name="message">The message to attach to the exception</param>
    /// <typeparam name="TApiException">The expected type of the response</typeparam>
    /// <returns>The example instance of type <typeparamref name="TApiException" /></returns>
    protected TApiException GetExamples<TApiException>(HttpStatusCode status, string message)
        where TApiException : ApiException, new() => FromSystemException<TApiException>(new(message), status);

    /// <summary>
    ///     This method generates a example instance of this type
    /// </summary>
    /// <returns>The example instance of this type</returns>
    public ApiException GetExamples() =>
        GetExamples<ApiException>(HttpStatusCode.InternalServerError, "An Error Occurred");
}
