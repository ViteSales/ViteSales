using Google.Apis.Auth.OAuth2;
using Google.Cloud.PubSub.V1;
using Grpc.Auth;
using Microsoft.Extensions.Options;
using ViteSales.ERP.SDK.Interfaces;
using ViteSales.ERP.SDK.Models;
using ViteSales.ERP.SDK.Utils;
using ViteSales.Shared.Models;
using AppSettings = ViteSales.Shared.Utils.AppSettings;

namespace ViteSales.ERP.SDK.Services.MessageQueue;

public class PubSub: IPubSub
{
    private readonly PublisherServiceApiClient _publisher;
    private readonly GoogleCredential _credential;
    private readonly AppSettings _secret;
    private readonly ConnectionConfig _config;
    private TopicName _topicName;
    private Subscriber _subscriber;
    
    public PubSub(IOptions<AppSettings> settings, IOptions<ConnectionConfig> config)
    {
        ArgumentNullException.ThrowIfNull(settings.Value);
        ArgumentNullException.ThrowIfNull(config.Value);
        _secret = settings.Value;
        _config = config.Value;
        _credential = _secret.GoogleCredential;
        _publisher = new PublisherServiceApiClientBuilder
        {
            ChannelCredentials = _credential.ToChannelCredentials()
        }.Build();
    }

    public async Task<Subscriber> InitTopicAsync(string tableName)
    {
        _topicName = TopicName.FromProjectTopic(projectId: _secret.GcpCredentials.ProjectId, topicId: Utility.GetTopicName(_config.Host, _config.Database));
        _subscriber = new Subscriber(_secret);
        await _subscriber.CreateSubscription(_topicName, Utility.QueueName(_config.Host, _config.Database, tableName),tableName);
        return _subscriber;
    }

    public async Task PublishAsync(string tableName, PubSubMessage message)
    {
        _topicName = TopicName.FromProjectTopic(projectId: _secret.GcpCredentials.ProjectId, topicId: Utility.GetTopicName(_config.Host, _config.Database));
        await _publisher.PublishAsync(_topicName, [new PubsubMessage { Data = message.ToByteString(), Attributes = { { "route", tableName} }}]);
    }
}