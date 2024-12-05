namespace ViteSales.Data.Entities;

public partial class FcrevalueDocument
{
    public long DtlKey { get; set; }

    public long FcrevalueKey { get; set; }

    public string Category { get; set; } = null!;

    public string DocType { get; set; } = null!;

    public long DocKey { get; set; }

    public DateTime DocDate { get; set; }

    public string DocNo { get; set; } = null!;

    public string AccNo { get; set; } = null!;

    public string? CompanyName { get; set; }

    public string CurrencyCode { get; set; } = null!;

    public decimal RevalueRate { get; set; }

    public decimal CurrencyRate { get; set; }

    public decimal Outstanding { get; set; }

    public decimal GainLoss { get; set; }

    public string? ProjNo { get; set; }

    public string? DeptNo { get; set; }

    public virtual Glmast AccNoNavigation { get; set; } = null!;

    public virtual Currency CurrencyCodeNavigation { get; set; } = null!;

    public virtual Dept? DeptNoNavigation { get; set; }

    public virtual Project? ProjNoNavigation { get; set; }
}
