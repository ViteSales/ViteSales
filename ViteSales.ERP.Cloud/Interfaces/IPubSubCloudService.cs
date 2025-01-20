using ViteSales.ERP.Shared.Models;

namespace ViteSales.ERP.Cloud.Interfaces;

public interface IPubSubCloudService
{
    public Task CreateTopicAsync(CloudIdentifierPair identifierPair);
    public Task DropTopicAsync(CloudIdentifierPair identifierPair);
}