namespace ViteSales.Data.Constants;

public static class AccountTypes
{
    public enum SpecialAccountType
    {
        Normal,
        Bank,
        Cash,
        Deposit,
        Others,
        FixedAsset,
        AccumulatedDepreciation,
        BalanceStock,
        OpenStock,
        CloseStock,
        DebtorControl,
        CreditorControl,
        RetainedEarning,
        Debtor,
        Creditor
    }
    
    public static SpecialAccountType ToSpecialAccountType(object obj)
    {
        return obj.ToString() switch
        {
            "SFA" => SpecialAccountType.FixedAsset,
            "SAD" => SpecialAccountType.AccumulatedDepreciation,
            "SBK" => SpecialAccountType.Bank,
            "SCH" => SpecialAccountType.Cash,
            "SDP" => SpecialAccountType.Deposit,
            "SOP" => SpecialAccountType.Others,
            "SBS" => SpecialAccountType.BalanceStock,
            "SOS" => SpecialAccountType.OpenStock,
            "SCS" => SpecialAccountType.CloseStock,
            "SDC" => SpecialAccountType.DebtorControl,
            "SCC" => SpecialAccountType.CreditorControl,
            "SRE" => SpecialAccountType.RetainedEarning,
            "SDR" => SpecialAccountType.Debtor,
            "SCR" => SpecialAccountType.Creditor,
            _ => SpecialAccountType.Normal,
        };
    }

    public static object FromSpecialAccountType(SpecialAccountType sat)
    {
        return sat switch
        {
            SpecialAccountType.FixedAsset => "SFA",
            SpecialAccountType.AccumulatedDepreciation => "SAD",
            SpecialAccountType.Bank => "SBK",
            SpecialAccountType.Cash => "SCH",
            SpecialAccountType.Deposit => "SDP",
            SpecialAccountType.Others => "SOP",
            SpecialAccountType.BalanceStock => "SBS",
            SpecialAccountType.OpenStock => "SOS",
            SpecialAccountType.CloseStock => "SCS",
            SpecialAccountType.DebtorControl => "SDC",
            SpecialAccountType.CreditorControl => "SCC",
            SpecialAccountType.RetainedEarning => "SRE",
            SpecialAccountType.Debtor => "SDR",
            SpecialAccountType.Creditor => "SCR",
            _ => DBNull.Value,
        };
    }
}