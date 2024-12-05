namespace ViteSales.Data.Entities;

public partial class PriceBookMatrix
{
    public long MatrixKey { get; set; }

    public string MatrixName { get; set; } = null!;

    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public string Y { get; set; } = null!;

    public string? Ycriteria { get; set; }

    public string? X { get; set; }

    public string? Xcriteria { get; set; }

    public string? UnitPrice { get; set; }

    public string? Discount { get; set; }

    public decimal? Geqty { get; set; }

    public string? IsActive { get; set; }

    public short? Priority { get; set; }

    public virtual ICollection<PriceBookRule> PriceBookRules { get; set; } = new List<PriceBookRule>();
}
