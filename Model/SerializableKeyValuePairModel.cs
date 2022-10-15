using System.Text.Json.Serialization;
using System.Xml.Serialization;
using Swashbuckle.AspNetCore.Filters;

// Define our namespace
namespace SyncStream.Exception.Api.Model;

/// <summary>
///     This class maintains the model structure for a serializable dictionary key/value pair
/// </summary>
/// <typeparam name="TKey">The expected type of the key</typeparam>
/// <typeparam name="TValue">The expected type of the value</typeparam>
[XmlRoot("keyValuePair")]
public class SerializableKeyValuePairModel<TKey, TValue> : IExamplesProvider<SerializableKeyValuePairModel<string, string>>
{
    /// <summary>
    ///     This method converts a dictionary to a list of serializable key/value pairs
    /// </summary>
    /// <param name="dictionary"></param>
    /// <returns></returns>
    public static List<SerializableKeyValuePairModel<TKey, TValue>>
        FromDictionary(Dictionary<TKey, TValue> dictionary) =>
        dictionary.ToList().Select(i => new SerializableKeyValuePairModel<TKey, TValue>(i)).ToList();

    /// <summary>
    ///     This property contains the key for the value
    /// </summary>
    [JsonPropertyName("key")]
    [XmlElement("key")]
    public TKey Key { get; set; }

    /// <summary>
    ///     This property contains the value
    /// </summary>
    [JsonPropertyName("value")]
    [XmlElement("value")]
    public TValue Value { get; set; }

    /// <summary>
    ///     This method instantiates an empty serializable key/value pair
    /// </summary>
    public SerializableKeyValuePairModel()
    {
    }

    /// <summary>
    ///     This method instantiates a serializable key/value pair with a <paramref name="key" /> and <paramref name="value" />
    /// </summary>
    /// <param name="key">The key for the pair</param>
    /// <param name="value">The value for the pair</param>
    public SerializableKeyValuePairModel(TKey key, TValue value)
    {
        // Set the key into the instance
        Key = key;

        // Set the value into the instance
        Value = value;
    }

    /// <summary>
    ///     This method instantiates a serializable key/value pair from n existing <paramref name="keyValuePair" />
    /// </summary>
    /// <param name="keyValuePair">The existing key/value pair</param>
    public SerializableKeyValuePairModel(KeyValuePair<TKey, TValue> keyValuePair) : this(keyValuePair.Key,
        keyValuePair.Value)
    {
    }

    /// <summary>
    ///     This method generates an example instance of this type
    /// </summary>
    /// <returns>The example instance of this type</returns>
    public SerializableKeyValuePairModel<string, string> GetExamples() => new("fubar", "bubaz");

    /// <summary>
    ///     This method converts the instance to a key/value pair
    /// </summary>
    /// <returns>The standard key/value pair</returns>
    public KeyValuePair<TKey, TValue> ToKeyValuePair() => new(Key, Value);
}
