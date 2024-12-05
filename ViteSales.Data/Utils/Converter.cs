namespace ViteSales.Data.Utils;

public static class Converter
{
    public static string BooleanToText(bool tf)
    {
        return tf ? "T" : "F";
    }

    public static bool TextToBoolean(object? obj)
    {
        if (obj == null || obj == DBNull.Value)
        {
            return false;
        }

        return string.Compare(obj.ToString(), "T", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(obj.ToString(), "True", StringComparison.OrdinalIgnoreCase) == 0;
    }

    public static string ToBooleanText(object obj)
    {
        if (obj == null || obj == DBNull.Value)
        {
            return "F";
        }

        return BooleanToText(Convert.ToBoolean(obj));
    }

    public static bool ToBoolean(object obj)
    {
        if (obj == null || obj == DBNull.Value)
        {
            return false;
        }

        return Convert.ToBoolean(obj);
    }

    public static DateTime ToDateTime(object obj)
    {
        if (obj == null || obj == DBNull.Value)
        {
            return DateTime.MinValue;
        }

        return Convert.ToDateTime(obj);
    }

    public static decimal ToDecimal(object obj)
    {
        if (obj == null || obj == DBNull.Value)
        {
            return 0m;
        }

        return Convert.ToDecimal(obj);
    }

    public static sbyte ToSByte(object obj)
    {
        if (obj == null || obj == DBNull.Value)
        {
            return 0;
        }

        return Convert.ToSByte(obj);
    }

    public static short ToInt16(object obj)
    {
        if (obj == null || obj == DBNull.Value)
        {
            return 0;
        }

        return Convert.ToInt16(obj);
    }

    public static int ToInt32(object obj)
    {
        if (obj == null || obj == DBNull.Value)
        {
            return 0;
        }

        return Convert.ToInt32(obj);
    }

    public static long ToInt64(object obj)
    {
        if (obj == null || obj == DBNull.Value)
        {
            return 0L;
        }

        return Convert.ToInt64(obj);
    }

    public static ulong ToUInt64(object obj)
    {
        if (obj == null || obj == DBNull.Value)
        {
            return 0uL;
        }

        return Convert.ToUInt64(obj);
    }

    public static Guid ToGuid(object obj)
    {
        if (obj == null || obj == DBNull.Value)
        {
            return Guid.Empty;
        }

        return (Guid)obj;
    }

    public static short? ToNullableInt16(object obj)
    {
        if (obj == null || obj == DBNull.Value)
        {
            return null;
        }

        return Convert.ToInt16(obj);
    }

    public static int? ToNullableInt32(object obj)
    {
        if (obj == null || obj == DBNull.Value)
        {
            return null;
        }

        return Convert.ToInt32(obj);
    }

    public static long? ToNullableInt64(object obj)
    {
        if (obj == null || obj == DBNull.Value)
        {
            return null;
        }

        return Convert.ToInt64(obj);
    }

    public static ulong? ToNullableUInt64(object obj)
    {
        if (obj == null || obj == DBNull.Value)
        {
            return 0uL;
        }

        return Convert.ToUInt64(obj);
    }

    public static decimal? ToNullableDecimal(object obj)
    {
        if (obj == null || obj == DBNull.Value)
        {
            return null;
        }

        return Convert.ToDecimal(obj);
    }

    public static string ToString(object obj)
    {
        if (obj == null || obj == DBNull.Value)
        {
            return null;
        }

        return obj.ToString();
    }

    public static DateTime? ToNullableDateTime(object obj)
    {
        if (obj == null || obj == DBNull.Value)
        {
            return null;
        }

        return Convert.ToDateTime(obj);
    }

    public static DateTime? ToNullableDate(object obj)
    {
        if (obj == null || obj == DBNull.Value)
        {
            return null;
        }

        return Convert.ToDateTime(obj).Date;
    }

    public static bool? ToNullableBoolean(object obj)
    {
        if (obj == null || obj == DBNull.Value)
        {
            return null;
        }

        return Convert.ToBoolean(obj);
    }

    public static decimal RoundToNearest5Cents(decimal d)
    {
        d = decimal.Round(d, 2, MidpointRounding.AwayFromZero);
        bool flag = false;
        if (d < 0m)
        {
            d = -d;
            flag = true;
        }

        decimal num = decimal.Round(d, 1, MidpointRounding.AwayFromZero);
        decimal num2 = num - d;
        if (num2 <= -0.03m)
        {
            num += 0.05m;
        }
        else if (num2 >= 0.03m)
        {
            num -= 0.05m;
        }

        return flag ? (-num) : num;
    }
}