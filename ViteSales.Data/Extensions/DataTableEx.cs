using System.Data;

namespace ViteSales.Data.Extensions;

public static class DataTableEx
{
    public static string ToXml(this DataTable dt)
    {
        using var sw = new StringWriter();
        dt.WriteXml(sw);
        return sw.ToString();
    }
}