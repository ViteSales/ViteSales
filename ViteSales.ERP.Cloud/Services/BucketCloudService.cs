using Google.Apis.Storage.v1.Data;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ViteSales.ERP.Cloud.Const;
using ViteSales.ERP.Cloud.Extensions;
using ViteSales.ERP.Cloud.Interfaces;
using ViteSales.ERP.Cloud.Models;
using ViteSales.ERP.Shared.Utils;

namespace ViteSales.ERP.Cloud.Services;

public class BucketCloudService: IBucketCloudService
{
    private readonly AppSettings _appSettings;
    private readonly StorageClient _storageClient;
    private readonly ILogger<DatabaseCloudService> _logger;
    
    public BucketCloudService(IOptions<AppSettings> appSettings, ILogger<DatabaseCloudService> logger)
    {
        ArgumentNullException.ThrowIfNull(appSettings.Value);
        _appSettings = appSettings.Value;
        _logger = logger;
        _storageClient = StorageClient.Create(_appSettings.GoogleCredential);
    }
    
    public async Task<BucketInfo> CreateBucket(string bucketName, Regions regions)
    {
        _logger.LogInformation("Creating bucket with name: {BucketName} in region: {Region}", bucketName, regions);
        try
        {
            var existingBucket = await _storageClient.GetBucketAsync(bucketName);
            _logger.LogInformation("Bucket already exists with ID: {BucketId}, Name: {BucketName}, Location: {BucketLocation}", 
                existingBucket.Id, existingBucket.Name, existingBucket.Location);

            return new BucketInfo
            {
                Id = existingBucket.Id,
                Name = existingBucket.Name,
                Location = existingBucket.Location,
            };
        }
        catch (Google.GoogleApiException ex) when (ex.Error.Code == 404)
        {
            _logger.LogInformation("Bucket does not exist. Creating a new bucket with name: {BucketName} in region: {Region}", bucketName, regions);

            var bucket = await _storageClient.CreateBucketAsync(_appSettings.GcpCredentials.ProjectId, new Bucket
            {
                Name = bucketName,
                Location = regions.GetBucketSlug(),
            }, new CreateBucketOptions
            {
                PredefinedAcl = PredefinedBucketAcl.PublicRead,
                PredefinedDefaultObjectAcl = PredefinedObjectAcl.PublicRead,
            });

            _logger.LogInformation("Bucket created successfully with ID: {BucketId}, Name: {BucketName}, Location: {BucketLocation}", bucket.Id, bucket.Name, bucket.Location);

            return new BucketInfo
            {
                Id = bucket.Id,
                Name = bucket.Name,
                Location = bucket.Location,
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the bucket with name: {BucketName}", bucketName);
            throw;
        }
    }

    public async Task DropBucket(BucketInfo bucket)
    {
        _logger.LogInformation("Dropping bucket with Name: {BucketName}, ID: {BucketId}", bucket.Name, bucket.Id);
        try
        {
            await _storageClient.DeleteBucketAsync(new Bucket
            {
                Name = bucket.Name,
                Id = bucket.Id,
            });
            
            _logger.LogInformation("Bucket with Name: {BucketName}, ID: {BucketId} dropped successfully", bucket.Name, bucket.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while dropping the bucket with Name: {BucketName}, ID: {BucketId}", bucket.Name, bucket.Id);
            throw;
        }
    }
}