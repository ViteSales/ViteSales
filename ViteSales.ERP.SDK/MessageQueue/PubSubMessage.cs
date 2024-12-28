using System.Text.Json;
using Google.Protobuf;

namespace ViteSales.ERP.SDK.MessageQueue;

[Serializable]
public class PubSubMessage
{
    public required string Action { get; set; }
    public required string Data { get; set; }
    public required string QueuedAt { get; set; }
    public required string QueuedBy { get; set; }

    public ByteString ToByteString()
    {
        return ByteString.CopyFromUtf8(JsonSerializer.Serialize(this));
    }
    
    public static PubSubMessage FromByteString(string byteString)
    {
        var message = JsonSerializer.Deserialize<PubSubMessage>(byteString);
        if (message == null)
        {
            throw new InvalidOperationException("Failed to deserialize the provided event byte string.");
        }
        return message;
    }
}