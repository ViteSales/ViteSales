namespace ViteSales.Data.Entities;

public partial class DocTemplate
{
    public long AutoKey { get; set; }

    public string TemplateName { get; set; } = null!;

    public string DocType { get; set; } = null!;

    public byte[]? Data { get; set; }

    public string IsDefault { get; set; } = null!;

    public DateTime CreatedTimeStamp { get; set; }

    public string CreatedUserId { get; set; } = null!;

    public virtual User CreatedUser { get; set; } = null!;

    public virtual ICollection<DocTemplateUser> DocTemplateUsers { get; set; } = new List<DocTemplateUser>();
}
