namespace ViteSales.Data.Entities;

public partial class MailDtl
{
    public long Id { get; set; }

    public long MailId { get; set; }

    public string Filename { get; set; } = null!;

    public byte[]? Binary { get; set; }

    public virtual Mail Mail { get; set; } = null!;
}
