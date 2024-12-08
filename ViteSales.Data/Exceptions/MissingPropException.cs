namespace ViteSales.Data.Exceptions;

public class MissingPropException<T>: ViteSalesDataException<T> where T: class
{
    public MissingPropException(string message) : base(message)
    {
    }
    public MissingPropException(string message, Exception innerException) : base(message, innerException)
    {
    }
    public MissingPropException(string message, T data) : base(message, data)
    {
    }
    public MissingPropException(string message, T auth, Dictionary<string, dynamic>? dbProfile) : base(message, auth, dbProfile)
    {
    }
}