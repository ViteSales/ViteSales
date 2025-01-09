namespace ViteSales.ERP.Shared.Exceptions;

[Serializable]
public class TaxCodeException<T>: ViteSalesDataException<T> where T: class
{
    public TaxCodeException(string message) : base(message)
    {
    }
    public TaxCodeException(string message, Exception innerException) : base(message, innerException)
    {
    }
    public TaxCodeException(string message, T data) : base(message, data)
    {
    }
    public TaxCodeException(string message, T auth, Dictionary<string, dynamic>? dbProfile) : base(message, auth, dbProfile)
    {
    }
}