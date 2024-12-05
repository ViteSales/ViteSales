using System.Runtime.Serialization;
using System.Text.Json;

namespace ViteSales.API.Exceptions;

[Serializable]
public class ViteSalesException<T>: Exception where T: class
{
    public Dictionary<string, dynamic> AdditionalData;
    public ViteSalesException(string message): base(message) {}
    public ViteSalesException(string message, Exception innerException): base(message, innerException){}
    public ViteSalesException(string message, T data) : base(message)
    {
        this.AdditionalData = new ()
        {
            {nameof(data), data}
        };
    }
    public ViteSalesException(string message, T auth, Dictionary<string, dynamic>? dbProfile): base(message)
    {
        this.AdditionalData = new ()
        {
            { nameof(auth), auth },
            { nameof(dbProfile), dbProfile ?? new Dictionary<string, dynamic>() }
        };
    }
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(AdditionalData), JsonSerializer.Serialize(AdditionalData));
    }
}