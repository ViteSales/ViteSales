namespace ViteSales.Data.Models;

public class SettingsGeneral
{
    public string Country { get; set; }
    public string LocalCurrencyCode { get; set; }
    public string LocalCurrencySymbol { get; set; }
    public int SearchCommandTimeout { get; set; }
    public int CommandTimeout { get; set; }
    public string DataInputEncoding { get; set; }
    public string LocalCurrencyName { get; set;}
}