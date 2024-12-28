namespace ViteSales.ERP.SDK.Models;

[Serializable]
public class QueueMessage
{
    public required string Action { get; set; }
    public required string Data { get; set; }
    public required string QueuedAt { get; set; }
    public required string QueuedBy { get; set; }
}