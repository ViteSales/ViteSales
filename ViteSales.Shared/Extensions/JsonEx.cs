using System.Text.Json;
using System.Text.Json.Nodes;

namespace ViteSales.Shared.Extensions;

public static class JsonEx
{
    public static object ToObjectInferred(this object json)
    {
        return json is JsonArray or JsonObject or JsonNode or JsonElement or JsonDocument or JsonValue or IEnumerable<object> ? JsonSerializer.Serialize(json) : json;
    }
}