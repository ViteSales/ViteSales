namespace ViteSales.Data.Entities;

public partial class DrprocessingDo
{
    public long DocKey { get; set; }

    public long Drpkey { get; set; }

    public long SodtlKey { get; set; }

    public long SodocKey { get; set; }

    public long? DodocKey { get; set; }

    public string? DodocNo { get; set; }

    public long? DodtlKey { get; set; }

    public string? ItemCode { get; set; }

    public decimal? DeliveryQty { get; set; }

    public string? DeliveryUom { get; set; }

    public decimal? DeliveryRate { get; set; }

    public string Kiv { get; set; } = null!;

    public string CreatedUserId { get; set; } = null!;

    public DateTime CreatedTimeStamp { get; set; }

    public DateTime LastModified { get; set; }

    public string LastModifiedUserId { get; set; } = null!;

    public int LastUpdate { get; set; }

    public virtual User CreatedUser { get; set; } = null!;

    public virtual ItemUom? ItemUom { get; set; }

    public virtual User LastModifiedUser { get; set; } = null!;
}
