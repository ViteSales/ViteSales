namespace ViteSales.ERP.SDK.Const;

public enum DbOperationTypes
{
    Insert,
    Update,
    Upsert,
    Delete
}

public static class DbOperationTypesExtensions
{
    public static string ToAction(this DbOperationTypes type)
    {
        switch (type)
        {
            case DbOperationTypes.Insert:
                return "CREATE";
            case DbOperationTypes.Update:
                return "UPDATE";
            case DbOperationTypes.Upsert:
                return "UPDATE";
            case DbOperationTypes.Delete:
                return "DELETE";
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
}