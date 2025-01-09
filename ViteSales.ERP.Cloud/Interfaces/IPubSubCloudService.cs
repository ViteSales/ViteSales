namespace ViteSales.ERP.Cloud.Interfaces;

public interface IPubSubCloudService
{
    public Task CreateTopic();
    public Task DropTopic();
}