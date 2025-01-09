using System.Runtime.Serialization;
using System.Text.Json;

namespace ViteSales.ERP.Shared.Exceptions;

[Serializable]
public class ViteSalesDataException<T>: Exception where T: class
{
    public Dictionary<string, dynamic> AdditionalData;
    public ViteSalesDataException(string message): base(message) {}
    public ViteSalesDataException(string message, Exception innerException): base(message, innerException){}
    public ViteSalesDataException(string message, T data) : base(message)
    {
        this.AdditionalData = new ()
        {
            {nameof(data), data}
        };
    }
    public ViteSalesDataException(string message, T auth, Dictionary<string, dynamic>? dbProfile): base(message)
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