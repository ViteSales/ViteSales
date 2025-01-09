namespace ViteSales.ERP.Shared.Exceptions;

[Serializable]
public class StreamingException<T>: ViteSalesDataException<T> where T: class
{
    public StreamingException(string message) : base(message)
    {
    }
    public StreamingException(string message, Exception innerException) : base(message, innerException)
    {
    }
    public StreamingException(string message, T data) : base(message, data)
    {
    }
    public StreamingException(string message, T auth, Dictionary<string, dynamic>? dbProfile) : base(message, auth, dbProfile)
    {
    }
}