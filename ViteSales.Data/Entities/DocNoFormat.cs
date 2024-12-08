namespace ViteSales.Data.Entities;

public partial class DocNoFormat
{
    public long AutoKey { get; set; }

    public string Name { get; set; } = null!;

    public string DocType { get; set; } = null!;

    public int NextNumber { get; set; }

    public string Format { get; set; } = null!;

    public string Sample { get; set; } = null!;

    public string IsDefault { get; set; } = null!;

    public string OneMonthOneSet { get; set; } = null!;

    public int? MaxNumber { get; set; }

    public virtual ICollection<DocNoFormatAccNo> DocNoFormatAccNos { get; set; } = new List<DocNoFormatAccNo>();

    public virtual ICollection<DocNoFormatUser> DocNoFormatUsers { get; set; } = new List<DocNoFormatUser>();

    public virtual ICollection<PaymentMethods> PaymentMethodPaymentFormatNameNavigations { get; set; } = new List<PaymentMethods>();

    public virtual ICollection<PaymentMethods> PaymentMethodReceiptFormatNameNavigations { get; set; } = new List<PaymentMethods>();
}
