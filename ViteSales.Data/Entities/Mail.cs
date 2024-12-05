namespace ViteSales.Data.Entities;

public partial class Mail
{
    public long Id { get; set; }

    public DateTime CreatedTime { get; set; }

    public string? Email { get; set; }

    public string? Ccemail { get; set; }

    public string? Bccemail { get; set; }

    public string? Subject { get; set; }

    public string? Message { get; set; }

    public string Success { get; set; } = null!;

    public string? ErrorMessage { get; set; }

    public virtual ICollection<MailDtl> MailDtls { get; set; } = new List<MailDtl>();
}
