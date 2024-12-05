namespace ViteSales.Data.Entities;

public partial class UserGroup
{
    public long AutoKey { get; set; }

    public string UserGroupId { get; set; } = null!;

    public string? Description { get; set; }

    public int LastUpdate { get; set; }

    public virtual ICollection<UsersGroup> UsersGroups { get; set; } = new List<UsersGroup>();
}
