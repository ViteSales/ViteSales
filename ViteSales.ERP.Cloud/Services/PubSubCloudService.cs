using Google.Apis.Auth.OAuth2;
using Google.Cloud.PubSub.V1;
using Google.Protobuf.WellKnownTypes;
using Grpc.Auth;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ViteSales.ERP.Cloud.Interfaces;
using ViteSales.ERP.Shared.Models;
using ViteSales.ERP.Shared.Utils;

namespace ViteSales.ERP.Cloud.Services;

public class PubSubCloudService: IPubSubCloudService
{
    private readonly PublisherServiceApiClient _publisher;
    private readonly  SubscriberServiceApiClient _subscriber;
    private readonly GoogleCredential _credential;
    private readonly AppSettings _secret;
    private readonly ILogger<PubSubCloudService> _logger;
    
    public PubSubCloudService(IOptions<AppSettings> settings, ILogger<PubSubCloudService> logger)
    {
        ArgumentNullException.ThrowIfNull(settings.Value);
        _secret = settings.Value;
        _logger = logger;
        _credential = _secret.GoogleCredential;
        _publisher = new PublisherServiceApiClientBuilder
        {
            ChannelCredentials = _credential.ToChannelCredentials()
        }.Build();
        _subscriber = new SubscriberServiceApiClientBuilder
        {
            ChannelCredentials = _credential.ToChannelCredentials()
        }.Build();
    }


    public async Task CreateTopic(CloudIdentifierPair identifierPair)
    {
        var identifier = Utility.GetTopicName(identifierPair.Cloud, identifierPair.Identifier);
        var topicName = TopicName.FromProjectTopic(projectId: _secret.GcpCredentials.ProjectId, topicId: identifier);
        var subscriptionName = SubscriptionName.FromProjectSubscription(projectId: _secret.GcpCredentials.ProjectId, subscriptionId: identifier);
        try
        {
            _logger.LogInformation("Attempting to create topic with name: {TopicName}", topicName);
            
            _logger.LogDebug("Preparing topic details for creation. TopicName: {TopicName}, ProjectId: {ProjectId}", topicName, _secret.GcpCredentials.ProjectId);
            await _publisher.CreateTopicAsync(new Topic
            {
                Name = topicName.ToString(),
                TopicName = topicName,
                MessageRetentionDuration = Duration.FromTimeSpan(TimeSpan.FromDays(15))
            });
            _logger.LogInformation("Topic created successfully: {TopicName}", topicName);
            
            var subscriptionSettings = new PushConfig
            {
                Attributes = { { "x-goog-pubsub-message-ordering", "true" } }
            };
            _logger.LogDebug("Setting up subscription with SubscriptionName: {SubscriptionName}", subscriptionName);
            await _subscriber.CreateSubscriptionAsync(new Subscription
            {
                Name = subscriptionName.ToString(),
                Topic = topicName.ToString(),
                ExpirationPolicy = new ExpirationPolicy { Ttl = Duration.FromTimeSpan(TimeSpan.FromDays(15)) },
                AckDeadlineSeconds = 60 * 10,
                PushConfig = subscriptionSettings,
                EnableMessageOrdering = true,
                EnableExactlyOnceDelivery = true,
                Filter = $"attributes.route = \"default\""
            });
            _logger.LogInformation("Subscription created successfully: {SubscriptionName}", subscriptionName);
        }
        catch (Grpc.Core.RpcException ex) when (ex.StatusCode == Grpc.Core.StatusCode.AlreadyExists)
        {
            _logger.LogWarning("Topic or Subscription already exists. Topic: {TopicName}, Subscription: {SubscriptionName}", topicName, subscriptionName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create topic or subscription. Topic: {TopicName}, Subscription: {SubscriptionName}", topicName, subscriptionName);
            throw;
        }
    }

    public async Task DropTopic(CloudIdentifierPair identifierPair)
    {
        var topicName = TopicName.FromProjectTopic(projectId: _secret.GcpCredentials.ProjectId, topicId: Utility.GetTopicName(identifierPair.Cloud, identifierPair.Identifier));
        try
        {
            var subscriptions = _publisher.ListTopicSubscriptions(topicName);
            foreach (var subscription in subscriptions)
            {
                if (subscription is null) continue;
                _logger.LogInformation("Attempting to delete subscription: {Subscription}", subscription);
                await _subscriber.DeleteSubscriptionAsync(subscription);
            }
            _logger.LogInformation("Attempting to delete topic: {TopicName}", topicName);
            await _publisher.DeleteTopicAsync(topicName);
            _logger.LogInformation("Successfully deleted topic: {TopicName}", topicName);
        }
        catch (Grpc.Core.RpcException ex) when (ex.StatusCode == Grpc.Core.StatusCode.NotFound)
        {
            _logger.LogWarning("Topic not found: {TopicName}", topicName);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to delete topic: {TopicName}", topicName);
            throw;
        }
    }
}