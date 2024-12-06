namespace ViteSales.Data.Constants;

public static class CashFlow
{
    public enum CashFlowCategory
    {
        Unassigned,
        OperatingActivities,
        InvestingActivities,
        FinancingActivities
    }
    
    public static CashFlowCategory ToCashFlowCategory(object obj)
    {
        return obj.ToString() switch
        {
            "O" => CashFlowCategory.OperatingActivities,
            "I" => CashFlowCategory.InvestingActivities,
            "F" => CashFlowCategory.FinancingActivities,
            _ => CashFlowCategory.Unassigned,
        };
    }

    public static object FromCashFlowCategory(CashFlowCategory cfc)
    {
        return cfc switch
        {
            CashFlowCategory.OperatingActivities => "O",
            CashFlowCategory.InvestingActivities => "I",
            CashFlowCategory.FinancingActivities => "F",
            _ => DBNull.Value,
        };
    }
}