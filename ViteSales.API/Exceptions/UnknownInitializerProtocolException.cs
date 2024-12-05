namespace ViteSales.API.Exceptions;

public class UnknownInitializerProtocolException<T>: ViteSalesException<T> where T: class
{

    public UnknownInitializerProtocolException(string message) : base(message)
    {
    }
    public UnknownInitializerProtocolException(string message, Exception innerException) : base(message, innerException)
    {
    }
    public UnknownInitializerProtocolException(string message, T auth) : base(message, auth)
    {
        
    }
    public UnknownInitializerProtocolException(string message, T auth, Dictionary<string, dynamic> dbProfile) : base(message, auth, dbProfile)
    {
    }
}