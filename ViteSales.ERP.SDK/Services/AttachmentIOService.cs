using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ViteSales.ERP.SDK.Interfaces;
using ViteSales.ERP.Shared.Models;
using ViteSales.ERP.Shared.Utils;

namespace ViteSales.ERP.SDK.Services;

public class AttachmentIOService: IAttachmentIOService
{
    private readonly StorageClient _storageClient;
    private readonly ConnectionConfig _connConfig;
    private readonly ILogger<AttachmentIOService> _logger;
    
    public AttachmentIOService(IOptions<AppSettings> appSettings, IOptions<ConnectionConfig> connConfig, ILogger<AttachmentIOService> logger)
    {
        ArgumentNullException.ThrowIfNull(appSettings.Value);
        ArgumentNullException.ThrowIfNull(connConfig.Value);
        _connConfig = connConfig.Value;
        _logger = logger;
        _storageClient = StorageClient.Create(appSettings.Value.GoogleCredential);

        _logger.LogInformation("AttachmentIOService initialized with bucket: {Bucket}", _connConfig.Bucket);
    }
    
    public async Task UploadObject(MemoryStream stream, string fileName)
    {
        try
        {
            _logger.LogInformation("Starting upload of file: {FileName} to bucket: {Bucket}", fileName, _connConfig.Bucket);
            await _storageClient.UploadObjectAsync(_connConfig.Bucket, fileName, "application/octet-stream", stream,
                new UploadObjectOptions
                {
                    PredefinedAcl = PredefinedObjectAcl.PublicRead
                });
            _logger.LogInformation("Successfully uploaded file: {FileName} to bucket: {Bucket}", fileName, _connConfig.Bucket);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while uploading file: {FileName} to bucket: {Bucket}", fileName, _connConfig.Bucket);
            throw;
        }
    }

    public async Task<Stream> DownloadObject(string fileName)
    {
        _logger.LogInformation("Starting download of file: {FileName} from bucket: {Bucket}", fileName, _connConfig.Bucket);
        var stream = new MemoryStream();
        try
        {
            await _storageClient.DownloadObjectAsync(_connConfig.Bucket, fileName, stream);
            _logger.LogInformation("Successfully downloaded file: {FileName} from bucket: {Bucket}", fileName, _connConfig.Bucket);
            stream.Position = 0; // Reset the memory stream position for further use
            return stream;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while downloading file: {FileName} from bucket: {Bucket}", fileName, _connConfig.Bucket);
            throw;
        }
    }
}