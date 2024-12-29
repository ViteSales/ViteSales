using System.Text.Json;
using System.Text.Json.Nodes;

namespace ViteSales.Shared.Extensions;

public static class JsonEx
{
    public static object ToObjectInferred(this object json)
    {
        return json is JsonArray or JsonObject or JsonNode or JsonElement or JsonDocument or JsonValue or IEnumerable<object> or IDictionary<string, object> ? JsonSerializer.Serialize(json) : json;
    }

    public static T? GetJsonPropertyValue<T>(this object json, string propertyName)
    {
        if (json is string jsonString)
        {
            try
            {
                json = JsonSerializer.Deserialize<JsonElement>(jsonString);
            }
            catch
            {
                throw new ArgumentException("The provided string is not a valid JSON.");
            }
        }

        if (json is JsonElement jsonElement)
        {
            if (jsonElement.TryGetProperty(propertyName, out var propertyValue))
            {
                if (propertyValue.ValueKind != JsonValueKind.Null && propertyValue.ValueKind != JsonValueKind.Undefined)
                {
                    return JsonSerializer.Deserialize<T>(propertyValue.GetRawText());
                }
            }
        }

        if (json is JsonNode jsonNode)
        {
            if (jsonNode[propertyName] is { } nodeValue)
            {
                return nodeValue.Deserialize<T>();
            }
        }

        if (json is JsonObject jsonObject)
        {
            if (jsonObject.TryGetPropertyValue(propertyName, out var propertyValue))
            {
                if (propertyValue is { } jsonValue)
                {
                    return jsonValue.Deserialize<T>();
                }
            }
        }

        if (json is Dictionary<string, object> dictionary)
        {
            if (dictionary.TryGetValue(propertyName, out var value))
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
        }

        if (json is JsonDocument jsonDocument)
        {
            if (jsonDocument.RootElement.TryGetProperty(propertyName, out var propertyValue))
            {
                if (propertyValue.ValueKind != JsonValueKind.Null && propertyValue.ValueKind != JsonValueKind.Undefined)
                {
                    return JsonSerializer.Deserialize<T>(propertyValue.GetRawText());
                }
            }
        }

        throw new ArgumentException($"The specified property '{propertyName}' could not be found in the JSON object.");
    }
}