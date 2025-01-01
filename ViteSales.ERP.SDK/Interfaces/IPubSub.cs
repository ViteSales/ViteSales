using ViteSales.ERP.SDK.Services.MessageQueue;

namespace ViteSales.ERP.SDK.Interfaces;

public interface IPubSub
{
    Task<Subscriber> InitTopicAsync(string topicName);
    Task PublishAsync(string topicName, PubSubMessage message);
    Task Drop(string topicName);
}