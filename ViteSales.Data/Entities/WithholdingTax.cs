namespace ViteSales.Data.Entities;

public partial class WithholdingTax
{
    public string WithholdingTaxCode { get; set; } = null!;

    public string? Description { get; set; }

    public decimal WithholdingTaxRate { get; set; }

    public string? WhtcreditableAccNo { get; set; }

    public string IsActive { get; set; } = null!;

    public int LastUpdate { get; set; }

    public string? WhtpayableAccNo { get; set; }

    public string Whtype { get; set; } = null!;

    public virtual ICollection<Creditor> CreditorWithholdingTaxCodeNavigations { get; set; } = new List<Creditor>();

    public virtual ICollection<Creditor> CreditorWithholdingVatcodeNavigations { get; set; } = new List<Creditor>();

    public virtual ICollection<Debtor> DebtorWithholdingTaxCodeNavigations { get; set; } = new List<Debtor>();

    public virtual ICollection<Debtor> DebtorWithholdingVatcodeNavigations { get; set; } = new List<Debtor>();

    public virtual Glmast? WhtcreditableAccNoNavigation { get; set; }

    public virtual Glmast? WhtpayableAccNoNavigation { get; set; }

    public virtual ICollection<WithholdingTaxEntryDtl> WithholdingTaxEntryDtls { get; set; } = new List<WithholdingTaxEntryDtl>();

    public virtual ICollection<WithholdingTaxPaymentDtl> WithholdingTaxPaymentDtls { get; set; } = new List<WithholdingTaxPaymentDtl>();

    public virtual ICollection<WithholdingTaxTran> WithholdingTaxTrans { get; set; } = new List<WithholdingTaxTran>();
}
