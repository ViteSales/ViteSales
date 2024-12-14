namespace ViteSales.Shared.Exceptions;

[Serializable]
public class TransactionLockException<T>: ViteSalesDataException<T> where T: class
{
    public TransactionLockException(string message) : base(message)
    {
    }
    public TransactionLockException(string message, Exception innerException) : base(message, innerException)
    {
    }
    public TransactionLockException(string message, T data) : base(message, data)
    {
    }
    public TransactionLockException(string message, T auth, Dictionary<string, dynamic>? dbProfile) : base(message, auth, dbProfile)
    {
    }
}