namespace ViteSales.Data.Entities;

public partial class UserScript
{
    public string ScriptName { get; set; } = null!;

    public string Language { get; set; } = null!;

    public string? Script { get; set; }

    public int LastUpdate { get; set; }

    public Guid Guid { get; set; }
}
