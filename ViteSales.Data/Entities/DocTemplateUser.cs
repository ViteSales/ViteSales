namespace ViteSales.Data.Entities;

public partial class DocTemplateUser
{
    public long AutoKey { get; set; }

    public string TemplateName { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public virtual DocTemplate TemplateNameNavigation { get; set; } = null!;
}
