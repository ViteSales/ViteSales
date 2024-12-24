using NpgsqlTypes;

namespace ViteSales.ERP.SDK.Models;

public class WhereClause
{
    public NpgsqlDbType DbType { get; set; }
    public required object Value { get; set; }
}