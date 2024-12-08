namespace ViteSales.Data.Models;

public class SettingsTransactionLock
{
    public bool LockTransaction { get; set; } = false;
    public string LockMessage { get; set; } = "";
}