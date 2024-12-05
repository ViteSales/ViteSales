namespace ViteSales.Data.Entities;

public partial class PhilippinesEwtremittance
{
    public int PeriodNo { get; set; }

    public DateTime? RemittanceDate { get; set; }

    public string? BankCode { get; set; }

    public string? Rorno { get; set; }

    public decimal? PreviousRemittedAmount { get; set; }

    public decimal? OverRemittance { get; set; }

    public decimal? Surcharge { get; set; }

    public decimal? Interest { get; set; }

    public decimal? Compromise { get; set; }

    public decimal? OtherPaymentMade { get; set; }
}
