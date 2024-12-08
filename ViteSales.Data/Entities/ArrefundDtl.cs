namespace ViteSales.Data.Entities;

public partial class ArrefundDtl
{
    public long DtlKey { get; set; }

    public long DocKey { get; set; }

    public int Seq { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public string? PaymentBy { get; set; }

    public decimal ToBankRate { get; set; }

    public string? ChequeNo { get; set; }

    public short? FloatDay { get; set; }

    public decimal? BankCharge { get; set; }

    public decimal? PaymentAmt { get; set; }

    public decimal? DebtorPaymentAmt { get; set; }

    public string IsRchq { get; set; } = null!;

    public DateTime? Rchqdate { get; set; }

    public long? BankChargeDtlKey { get; set; }

    public decimal? LocalPaymentAmt { get; set; }

    public string? BankChargeProjNo { get; set; }

    public string? BankChargeDeptNo { get; set; }

    public string? BankChargeTaxCode { get; set; }

    public decimal? BankChargeTaxRate { get; set; }

    public decimal? BankChargeTax { get; set; }

    public string? BankChargeTaxRefNo { get; set; }

    public virtual Dept? BankChargeDeptNoNavigation { get; set; }

    public virtual Project? BankChargeProjNoNavigation { get; set; }

    public virtual TaxCode? BankChargeTaxCodeNavigation { get; set; }

    public virtual PaymentMethods PaymentMethodsNavigation { get; set; } = null!;
}
