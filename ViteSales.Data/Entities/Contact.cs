namespace ViteSales.Data.Entities;

public partial class Contact
{
    public long AutoKey { get; set; }

    public string AccNo { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Department { get; set; }

    public string? Title { get; set; }

    public string? MobilePhone { get; set; }

    public string? DirectPhone { get; set; }

    public string? DirectFax { get; set; }

    public string? EmailAddress { get; set; }

    public string? Imaddress { get; set; }

    public string? Note { get; set; }

    public int? OpeningBonusPoint { get; set; }

    public int LastUpdate { get; set; }

    public string? IncludeInContactInfo { get; set; }

    public virtual Glmast AccNoNavigation { get; set; } = null!;
}
