namespace ViteSales.Data.Constants;

public static class CashBook
{
    public enum CashBookType
    {
        CashReceipt,
        CashPayment
    }

    public static string GetString(this CashBookType type)
    {
        switch (type)
        {
            case CashBookType.CashReceipt:
                return "OR";
            case CashBookType.CashPayment:
                return "PV";
            default:
                return "";
        }
    }
}