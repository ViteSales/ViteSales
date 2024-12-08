namespace ViteSales.Data.Exceptions;

public class EmptyDataException<T>: ViteSalesDataException<T> where T: class
{
    public EmptyDataException(string message) : base(message)
    {
    }
    public EmptyDataException(string message, Exception innerException) : base(message, innerException)
    {
    }
    public EmptyDataException(string message, T auth) : base(message, auth)
    {
        
    }
    public EmptyDataException(string message, T auth, Dictionary<string, dynamic> dbProfile) : base(message, auth, dbProfile)
    {
    }
}