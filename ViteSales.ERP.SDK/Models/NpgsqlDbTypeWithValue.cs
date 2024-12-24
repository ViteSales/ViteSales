using NpgsqlTypes;

namespace ViteSales.ERP.SDK.Models;

public class NpgsqlDbTypeWithValue
{
    public required string Parameter { get; set; }
    public NpgsqlDbType DbType { get; set; }
    public required object Value { get; set; }
}