namespace ViteSales.Data.Entities;

public partial class Comment
{
    public long CommentKey { get; set; }

    public byte CommentType { get; set; }

    public DateTime CommentDateTime { get; set; }

    public string? UserId { get; set; }

    public string? DocType { get; set; }

    public long? DocKey { get; set; }

    public string? Comment1 { get; set; }

    public virtual User? User { get; set; }
}
