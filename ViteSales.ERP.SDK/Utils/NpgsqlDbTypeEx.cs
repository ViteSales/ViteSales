using NpgsqlTypes;

namespace ViteSales.ERP.SDK.Utils;

public static class NpgsqlDbTypeEx
{
    public static NpgsqlDbType ToNpgsqlDbType(this object obj)
    {
        
        return obj switch
        {
            bool => NpgsqlDbType.Boolean,
            byte => NpgsqlDbType.Smallint,
            sbyte => NpgsqlDbType.Smallint,
            short => NpgsqlDbType.Smallint,
            ushort => NpgsqlDbType.Integer,
            int => NpgsqlDbType.Integer,
            uint => NpgsqlDbType.Bigint,
            long => NpgsqlDbType.Bigint,
            ulong => NpgsqlDbType.Numeric,
            float => NpgsqlDbType.Real,
            double => NpgsqlDbType.Double,
            decimal => NpgsqlDbType.Numeric,
            string => NpgsqlDbType.Text,
            char => NpgsqlDbType.Char,
            DateTime => NpgsqlDbType.Timestamp,
            DateTimeOffset => NpgsqlDbType.TimestampTz,
            TimeSpan => NpgsqlDbType.Interval,
            Guid => NpgsqlDbType.Uuid,
            byte[] => NpgsqlDbType.Bytea,
            IEnumerable<char> => NpgsqlDbType.Text,
            _ => throw new ArgumentException($"Unsupported object type: {obj.GetType().FullName}")
        };
    }
}