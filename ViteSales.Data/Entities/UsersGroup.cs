namespace ViteSales.Data.Entities;

public partial class UsersGroup
{
    public string UserId { get; set; } = null!;

    public string UserGroupId { get; set; } = null!;

    public long AutoKey { get; set; }

    public virtual UserGroup UserGroup { get; set; } = null!;
}
