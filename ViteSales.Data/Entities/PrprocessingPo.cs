namespace ViteSales.Data.Entities;

public partial class PrprocessingPo
{
    public long DocKey { get; set; }

    public long Prpkey { get; set; }

    public long FromDtlKey { get; set; }

    public long FromDocKey { get; set; }

    public long? RefDocKey { get; set; }

    public string? RefDocNo { get; set; }

    public string? RefDocType { get; set; }

    public long? RefDtlKey { get; set; }

    public string? CreditorCode { get; set; }

    public string? ItemCode { get; set; }

    public decimal? OrderQty { get; set; }

    public string? OrderUom { get; set; }

    public decimal? OrderRate { get; set; }

    public string Kiv { get; set; } = null!;

    public string CreatedUserId { get; set; } = null!;

    public DateTime CreatedTimeStamp { get; set; }

    public DateTime LastModified { get; set; }

    public string LastModifiedUserId { get; set; } = null!;

    public int LastUpdate { get; set; }

    public string? FromDocType { get; set; }

    public virtual User CreatedUser { get; set; } = null!;

    public virtual Glmast? CreditorCodeNavigation { get; set; }

    public virtual ItemUom? ItemUom { get; set; }

    public virtual User LastModifiedUser { get; set; } = null!;
}
