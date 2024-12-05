using System.Text.Json;
using System.Text.Json.Serialization;

namespace ViteSales.API.Utils;

public class JsonDateTimeConverter: JsonConverter<DateTime>
{
    private const string DateFormat = "yyyy-MM-ddTHH:mm:ssZ";

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateTime.ParseExact(reader.GetString(), DateFormat, null);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToUniversalTime().ToString(DateFormat));
    }
}