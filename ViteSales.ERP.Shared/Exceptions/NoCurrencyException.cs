namespace ViteSales.ERP.Shared.Exceptions;

[Serializable]
public class NoCurrencyException<T>: ViteSalesDataException<T> where T: class
{
    public NoCurrencyException(string message) : base(message)
    {
    }
    public NoCurrencyException(string message, Exception innerException) : base(message, innerException)
    {
    }
    public NoCurrencyException(string message, T data) : base(message, data)
    {
    }
    public NoCurrencyException(string message, T auth, Dictionary<string, dynamic>? dbProfile) : base(message, auth, dbProfile)
    {
    }
}