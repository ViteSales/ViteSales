namespace ViteSales.Data.Entities;

public partial class SalesAtc
{
    public string Atc { get; set; } = null!;

    public string? Description { get; set; }

    public string IsActive { get; set; } = null!;

    public int LastUpdate { get; set; }

    public virtual ICollection<Arcndtl> Arcndtls { get; set; } = new List<Arcndtl>();

    public virtual ICollection<Ardndtl> Ardndtls { get; set; } = new List<Ardndtl>();

    public virtual ICollection<ArinvoiceDtl> ArinvoiceDtls { get; set; } = new List<ArinvoiceDtl>();

    public virtual ICollection<Cbdtl> Cbdtls { get; set; } = new List<Cbdtl>();

    public virtual ICollection<Cndtl> Cndtls { get; set; } = new List<Cndtl>();

    public virtual ICollection<CashSalesdtl> Csdtls { get; set; } = new List<CashSalesdtl>();

    public virtual ICollection<Dndtl> Dndtls { get; set; } = new List<Dndtl>();

    public virtual ICollection<Ivdtl> Ivdtls { get; set; } = new List<Ivdtl>();

    public virtual ICollection<Jedtl> Jedtls { get; set; } = new List<Jedtl>();

    public virtual ICollection<TaxTrans> TaxTrans { get; set; } = new List<TaxTrans>();

    public virtual ICollection<TaxTransAuditDtl> TaxTransAuditDtls { get; set; } = new List<TaxTransAuditDtl>();

    public virtual ICollection<TaxTransCancelled> TaxTransCancelleds { get; set; } = new List<TaxTransCancelled>();
}
