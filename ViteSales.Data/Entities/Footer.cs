namespace ViteSales.Data.Entities;

public partial class Footer
{
    public long AutoKey { get; set; }

    public string FooterName { get; set; } = null!;

    public string? Caption { get; set; }

    public string AddToNetTotal { get; set; } = null!;

    public string AddToAnalysisNetTotal { get; set; } = null!;

    public string? Formula { get; set; }

    public string PostToGl { get; set; } = null!;

    public string? AccNo { get; set; }

    public string Enable { get; set; } = null!;

    public string? TaxCode { get; set; }

    public string? ParamCaption { get; set; }

    public string ParamVisible { get; set; } = null!;

    public short ParamDecimal { get; set; }

    public decimal? ParamDefaultValue { get; set; }

    public string ParamFromPercent { get; set; } = null!;

    public string? IsCalcBonusPoint { get; set; }

    public string? AddToCalculateExtraDiscountAmount { get; set; }

    public virtual Glmast? AccNoNavigation { get; set; }

    public virtual TaxCodes? TaxCodeNavigation { get; set; }
}
