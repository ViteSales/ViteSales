using ViteSales.ERP.SDK.Services.MessageQueue;

namespace ViteSales.ERP.SDK.Interfaces;

public interface IPubSub
{
    Task<Subscriber> InitTopicAsync(string tableName);
    Task PublishAsync(string tableName, PubSubMessage message);
}