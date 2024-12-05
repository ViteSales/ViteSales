namespace ViteSales.API.Exceptions;

public class MissingFieldsException<T>: ViteSalesException<T> where T: class
{
    public MissingFieldsException(string message) : base(message)
    {
    }
    public MissingFieldsException(string message, Exception innerException) : base(message, innerException)
    {
    }
    public MissingFieldsException(string message, T data) : base(message, data)
    {
    }
    public MissingFieldsException(string message, T auth, Dictionary<string, dynamic>? dbProfile) : base(message, auth, dbProfile)
    {
    }
}