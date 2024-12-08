using ViteSales.Data.Exceptions;

namespace ViteSales.ERP.GL.Exceptions;

public class FiscalYearOutOfRangeException<T>: ViteSalesDataException<T> where T: class
{
    public FiscalYearOutOfRangeException(string message) : base(message)
    {
    }
    public FiscalYearOutOfRangeException(string message, Exception innerException) : base(message, innerException)
    {
    }
    public FiscalYearOutOfRangeException(string message, T data) : base(message, data)
    {
    }
    public FiscalYearOutOfRangeException(string message, T auth, Dictionary<string, dynamic>? dbProfile) : base(message, auth, dbProfile)
    {
    }
}