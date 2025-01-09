namespace ViteSales.Shared.Models;

public class ConnectionConfig
{
    public string Host { get; set; } = null!;
    public string User { get; set; } = null!;
    public int Port { get; set; } = 5432;
    public string Password { get; set; } = null!;
    public string Database { get; set; } = null!;
}