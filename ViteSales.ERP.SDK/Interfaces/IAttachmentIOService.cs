namespace ViteSales.ERP.SDK.Interfaces;

public interface IAttachmentIOService
{
    Task UploadObjectAsync(MemoryStream stream, string fileName);
    Task<Stream> DownloadObjectAsync(string fileName);
}