using ViteSales.ERP.Shared.Models;

namespace ViteSales.ERP.Cloud.Interfaces;

public interface IPubSubCloudService
{
    public Task CreateTopic(CloudIdentifierPair identifierPair);
    public Task DropTopic(CloudIdentifierPair identifierPair);
}