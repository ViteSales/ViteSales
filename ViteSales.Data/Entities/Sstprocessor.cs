namespace ViteSales.Data.Entities;

public partial class Sstprocessor
{
    public long Sstkey { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public byte Duration { get; set; }

    public string Submitted { get; set; } = null!;

    public byte[]? TaxDataReport { get; set; }

    public long? JedocKey { get; set; }

    public string? DeclarantName { get; set; }

    public string? IcorPassportNo { get; set; }

    public string? DeclarantDesignation { get; set; }

    public string? TelephoneNo { get; set; }

    public decimal? PenaltyRate { get; set; }

    public string Compressed { get; set; } = null!;

    public long? ParentSstkey { get; set; }

    public int? Seq { get; set; }

    public string? ProductVersion { get; set; }

    public int Sstversion { get; set; }

    public DateTime? DeclarationDate { get; set; }

    public DateTime? CreatedTimeStamp { get; set; }

    public string? CreatedUserId { get; set; }
}
