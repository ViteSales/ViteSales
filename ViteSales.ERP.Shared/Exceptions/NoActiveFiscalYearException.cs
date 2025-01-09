namespace ViteSales.ERP.Shared.Exceptions;

[Serializable]
public class NoActiveFiscalYearException<T>: ViteSalesDataException<T> where T: class
{
    public NoActiveFiscalYearException(string message) : base(message)
    {
    }
    public NoActiveFiscalYearException(string message, Exception innerException) : base(message, innerException)
    {
    }
    public NoActiveFiscalYearException(string message, T data) : base(message, data)
    {
    }
    public NoActiveFiscalYearException(string message, T auth, Dictionary<string, dynamic>? dbProfile) : base(message, auth, dbProfile)
    {
    }
}