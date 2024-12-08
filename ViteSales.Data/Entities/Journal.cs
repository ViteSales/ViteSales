namespace ViteSales.Data.Entities;

public partial class Journal
{
    public long AutoKey { get; set; }

    public string JournalType { get; set; } = null!;

    public string EntryType { get; set; } = null!;

    public string? Description { get; set; }

    public string? Desc2 { get; set; }

    public virtual ICollection<Apcn> Apcns { get; set; } = new List<Apcn>();

    public virtual ICollection<Apdn> Apdns { get; set; } = new List<Apdn>();

    public virtual ICollection<Apinvoice> Apinvoices { get; set; } = new List<Apinvoice>();

    public virtual ICollection<Arapcontra> Arapcontras { get; set; } = new List<Arapcontra>();

    public virtual ICollection<Arcn> Arcns { get; set; } = new List<Arcn>();

    public virtual ICollection<Ardn> Ardns { get; set; } = new List<Ardn>();

    public virtual ICollection<Arinvoice> Arinvoices { get; set; } = new List<Arinvoice>();

    public virtual ICollection<Currency> Currencies { get; set; } = new List<Currency>();

    public virtual ICollection<Fcrevalue> Fcrevalues { get; set; } = new List<Fcrevalue>();

    public virtual ICollection<Gldtl> Gldtls { get; set; } = new List<Gldtl>();

    public virtual ICollection<Je> Jes { get; set; } = new List<Je>();

    public virtual ICollection<PaymentMethods> PaymentMethods { get; set; } = new List<PaymentMethods>();

    public virtual ICollection<UnrealizedGainLoss> UnrealizedGainLosses { get; set; } = new List<UnrealizedGainLoss>();
}
