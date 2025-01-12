using ViteSales.ERP.Cloud.Const;
using ViteSales.ERP.Cloud.Models;

namespace ViteSales.ERP.Cloud.Interfaces;

public interface IBucketCloudService
{
    public Task<BucketInfo> CreateBucket(string bucketName, Regions regions);
    public Task DropBucket(BucketInfo bucket);
}