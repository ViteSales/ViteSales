namespace ViteSales.Data.Models;

public class SettingsOpeningBalance
{
    public bool OpeningBalanceInYtd { get; set; } = true;
    public bool CanDirectEditArapOpeningBalance { get; set; } = false;
    public bool LockOpeningBalance { get; set;} = false;
}