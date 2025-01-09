using Google.Apis.Auth.OAuth2;
using Google.Cloud.PubSub.V1;
using Grpc.Auth;
using Microsoft.Extensions.Options;
using ViteSales.ERP.SDK.Interfaces;
using ViteSales.Shared.Utils;

namespace ViteSales.ERP.SDK.Services.MessageQueue;

public class PubSub: IPubSub
{
    private readonly PublisherServiceApiClient _publisher;
    private readonly GoogleCredential _credential;
    private readonly AppSettings _secret;
    private TopicName _topicName;
    private Subscriber _subscriber;
    
    public PubSub(IOptions<AppSettings> settings)
    {
        ArgumentNullException.ThrowIfNull(settings.Value);
        _secret = settings.Value;
        _credential = _secret.GoogleCredential;
        _publisher = new PublisherServiceApiClientBuilder
        {
            ChannelCredentials = _credential.ToChannelCredentials()
        }.Build();
    }

    public async Task<Subscriber> InitTopicAsync(string topicName)
    {
        var subscriptionName = $"{topicName}._subscription_";
        _topicName = TopicName.FromProjectTopic(projectId: _secret.GcpCredentials.ProjectId, topicId: topicName);
        _subscriber = new Subscriber(_secret);
        try
        {
            await _publisher.CreateTopicAsync(new Topic
            {
                Name = _topicName.ToString(),
                TopicName = _topicName
            });
            await _subscriber.CreateSubscription(_topicName, subscriptionName);
        }
        catch (Grpc.Core.RpcException ex) when (ex.StatusCode == Grpc.Core.StatusCode.AlreadyExists)
        {
            var topic = await _publisher.GetTopicAsync(_topicName);
            _topicName = topic.TopicName;
            await _subscriber.CreateSubscription(_topicName, subscriptionName);
        }
        catch (Grpc.Core.RpcException ex) when (ex.StatusCode != Grpc.Core.StatusCode.AlreadyExists)
        {
            throw;
        }
        return _subscriber;
    }

    public async Task PublishAsync(string topicName, PubSubMessage message)
    {
        _topicName = TopicName.FromProjectTopic(projectId: _secret.GcpCredentials.ProjectId, topicId: topicName);
        await _publisher.PublishAsync(_topicName, [new PubsubMessage { Data = message.ToByteString() }]);
    }

    public async Task Drop(string topicName)
    {
        var topic = TopicName.FromProjectTopic(projectId: _credential.QuotaProject, topicId: topicName);
        await InitTopicAsync(topicName);
        await _publisher.DeleteTopicAsync(topic);
        await _subscriber.Stop();
        await _subscriber.Drop();
    }
}