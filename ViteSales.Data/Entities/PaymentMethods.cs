namespace ViteSales.Data.Entities;

public partial class PaymentMethods
{
    public long AutoKey { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public string BankAccount { get; set; } = null!;

    public string? BankChargeAccount { get; set; }

    public decimal BankChargePercent { get; set; }

    public string MergeBankChargeTrans { get; set; } = null!;

    public string SpecialAccType { get; set; } = null!;

    public string JournalType { get; set; } = null!;

    public string AcceptChequeNo { get; set; } = null!;

    public string? PaymentBy { get; set; }

    public decimal Odlimit { get; set; }

    public string PaymentFormatName { get; set; } = null!;

    public string ReceiptFormatName { get; set; } = null!;

    public string? NextChequeNo { get; set; }

    public string IsActive { get; set; } = null!;

    public int LastUpdate { get; set; }

    public string? PaymentType { get; set; }

    public decimal? MinBankCharge { get; set; }

    public string? BankChargeTaxCode { get; set; }

    public Guid Guid { get; set; }

    public int? BankChargeTaxEntityId { get; set; }

    public virtual ICollection<ApdepositPaymentDtl> ApdepositPaymentDtls { get; set; } = new List<ApdepositPaymentDtl>();

    public virtual ICollection<ApdepositRefundPaymentDtl> ApdepositRefundPaymentDtls { get; set; } = new List<ApdepositRefundPaymentDtl>();

    public virtual ICollection<Apdeposit> Apdeposits { get; set; } = new List<Apdeposit>();

    public virtual ICollection<AppaymentDtl> AppaymentDtls { get; set; } = new List<AppaymentDtl>();

    public virtual ICollection<AprefundDtl> AprefundDtls { get; set; } = new List<AprefundDtl>();

    public virtual ICollection<ArdepositPaymentDtl> ArdepositPaymentDtls { get; set; } = new List<ArdepositPaymentDtl>();

    public virtual ICollection<ArdepositRefundPaymentDtl> ArdepositRefundPaymentDtls { get; set; } = new List<ArdepositRefundPaymentDtl>();

    public virtual ICollection<Ardeposit> Ardeposits { get; set; } = new List<Ardeposit>();

    public virtual ICollection<ArpaymentDtl> ArpaymentDtls { get; set; } = new List<ArpaymentDtl>();

    public virtual ICollection<ArrefundDtl> ArrefundDtls { get; set; } = new List<ArrefundDtl>();

    public virtual Glmast BankAccountNavigation { get; set; } = null!;

    public virtual Glmast? BankChargeAccountNavigation { get; set; }

    public virtual TaxCode? BankChargeTaxCodeNavigation { get; set; }

    public virtual TaxEntity? BankChargeTaxEntity { get; set; }

    public virtual ICollection<CbpaymentDtl> CbpaymentDtls { get; set; } = new List<CbpaymentDtl>();

    public virtual Journal JournalTypeNavigation { get; set; } = null!;

    public virtual ICollection<Location> LocationCashPaymentMethodNavigations { get; set; } = new List<Location>();

    public virtual ICollection<Location> LocationChequePaymentMethodNavigations { get; set; } = new List<Location>();

    public virtual ICollection<Location> LocationDebitCardPaymentMethodNavigations { get; set; } = new List<Location>();

    public virtual ICollection<Location> LocationPointPaymentMethodNavigations { get; set; } = new List<Location>();

    public virtual ICollection<Location> LocationVoucherPaymentMethodNavigations { get; set; } = new List<Location>();

    public virtual DocNoFormat PaymentFormatNameNavigation { get; set; } = null!;

    public virtual DocNoFormat ReceiptFormatNameNavigation { get; set; } = null!;
}
