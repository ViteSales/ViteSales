using ViteSales.ERP.Cloud.Const;
using ViteSales.ERP.Cloud.Models;

namespace ViteSales.ERP.Cloud.Interfaces;

public interface IBucketCloudService
{
    public Task<BucketInfo> CreateBucketAsync(string bucketName, Regions regions);
    public Task DropBucketAsync(BucketInfo bucket);
}