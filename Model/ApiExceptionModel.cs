using System.Net;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using Microsoft.OpenApi.Any;

// Define our namespace
namespace SyncStream.Exception.Api.Model;

/// <summary>
///     This class maintains the API model structure for our exceptions
/// </summary>
[XmlInclude(typeof(ApiExceptionTraceModel))]
[XmlRoot("exception")]
public class ApiExceptionModel
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
    [JsonPropertyName("status")]
    [XmlAttribute("status")]
    public HttpStatusCode Status { get; set; } = HttpStatusCode.InternalServerError;

    /// <summary>
    ///     This property contains the exception trace, if there is one
    /// </summary>
    [JsonPropertyName("trace")]
    [XmlElement("trace")]
    public List<ApiExceptionTraceModel> Trace { get; set; } = new();
}
