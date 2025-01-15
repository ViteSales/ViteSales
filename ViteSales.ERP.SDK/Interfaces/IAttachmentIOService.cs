namespace ViteSales.ERP.SDK.Interfaces;

public interface IAttachmentIOService
{
    Task UploadObject(MemoryStream stream, string fileName);
    Task<Stream> DownloadObject(string fileName);
}