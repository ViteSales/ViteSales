namespace ViteSales.Data.Entities;

public partial class Tariff
{
    public long AutoKey { get; set; }

    public string TariffCode { get; set; } = null!;

    public string? Description { get; set; }

    public string? Desc2 { get; set; }

    public string IsActive { get; set; } = null!;

    public int LastUpdate { get; set; }

    public Guid Guid { get; set; }

    public virtual ICollection<Apcndtl> Apcndtls { get; set; } = new List<Apcndtl>();

    public virtual ICollection<Apdndtl> Apdndtls { get; set; } = new List<Apdndtl>();

    public virtual ICollection<ApinvoiceDtl> ApinvoiceDtls { get; set; } = new List<ApinvoiceDtl>();

    public virtual ICollection<Arcndtl> Arcndtls { get; set; } = new List<Arcndtl>();

    public virtual ICollection<Ardndtl> Ardndtls { get; set; } = new List<Ardndtl>();

    public virtual ICollection<ArinvoiceDtl> ArinvoiceDtls { get; set; } = new List<ArinvoiceDtl>();

    public virtual ICollection<Cbdtl> Cbdtls { get; set; } = new List<Cbdtl>();

    public virtual ICollection<Cndtl> Cndtls { get; set; } = new List<Cndtl>();

    public virtual ICollection<Cpdtl> Cpdtls { get; set; } = new List<Cpdtl>();

    public virtual ICollection<CashSalesdtl> Csdtls { get; set; } = new List<CashSalesdtl>();

    public virtual ICollection<Dndtl> Dndtls { get; set; } = new List<Dndtl>();

    public virtual ICollection<Glmast> Glmasts { get; set; } = new List<Glmast>();

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();

    public virtual ICollection<Ivdtl> Ivdtls { get; set; } = new List<Ivdtl>();

    public virtual ICollection<Jedtl> Jedtls { get; set; } = new List<Jedtl>();

    public virtual ICollection<Pidtl> Pidtls { get; set; } = new List<Pidtl>();

    public virtual ICollection<Prdtl> Prdtls { get; set; } = new List<Prdtl>();

    public virtual ICollection<TaxTran> TaxTrans { get; set; } = new List<TaxTran>();

    public virtual ICollection<TaxTransAuditDtl> TaxTransAuditDtls { get; set; } = new List<TaxTransAuditDtl>();

    public virtual ICollection<TaxTransCancelled> TaxTransCancelleds { get; set; } = new List<TaxTransCancelled>();
}
