using Google.Cloud.Iam.V1;
using Google.Cloud.PubSub.V1;
using Google.Protobuf.WellKnownTypes;
using Grpc.Auth;
using Grpc.Core;
using ViteSales.ERP.SDK.Utils;

namespace ViteSales.ERP.SDK.Services.MessageQueue;

public sealed class Subscriber
{
    private readonly  SubscriberServiceApiClient? _subscriberClient;
    private readonly AppSettings _secret;
    private SubscriptionName _subscriptionName;
    private SubscriberClient _subscriber;
    
    public Subscriber(AppSettings settings)
    {
        _secret = settings;
        _subscriberClient = new SubscriberServiceApiClientBuilder
        {
            ChannelCredentials = _secret.GoogleCredential.ToChannelCredentials()
        }.Build();
    }

    public async Task CreateSubscription(TopicName topicName, string subId)
    {
        _subscriptionName = SubscriptionName.FromProjectSubscription(projectId: _secret.GcpCredentials.ProjectId, subscriptionId: subId);
        try
        {
            var subscriptionSettings = new PushConfig
            {
                Attributes = { { "x-goog-pubsub-message-ordering", "true" } }
            };
            await _subscriberClient.CreateSubscriptionAsync(new Subscription
            {
                Name = _subscriptionName.ToString(),
                Topic = topicName.ToString(),
                ExpirationPolicy = new ExpirationPolicy { Ttl = Duration.FromTimeSpan(TimeSpan.FromDays(7)) },
                AckDeadlineSeconds = 60 * 10,
                PushConfig = subscriptionSettings,
                EnableMessageOrdering = true,
                EnableExactlyOnceDelivery = true
            });
        }
        catch (Grpc.Core.RpcException ex) when (ex.StatusCode == Grpc.Core.StatusCode.AlreadyExists)
        {
            await _subscriberClient.GetSubscriptionAsync(_subscriptionName);
        }
        catch (Grpc.Core.RpcException ex) when (ex.StatusCode != Grpc.Core.StatusCode.AlreadyExists)
        {
            throw;
        }
    }

    public async Task Listen()
    {
        _subscriber = await SubscriberClient.CreateAsync(_subscriptionName);
        await _subscriber.StartAsync((message, cancellationToken) =>
        {
            try
            {
                var pubSubMessage = PubSubMessage.FromByteString(message.Data.ToStringUtf8());
                OnThresholdReached(new ThresholdReachedEventArgs { Message = pubSubMessage });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return Task.FromResult(SubscriberClient.Reply.Ack);
        });
    }

    public async Task Stop()
    {
        await _subscriber.StopAsync(CancellationToken.None);
    }

    public async Task Drop()
    {
        await _subscriberClient.DeleteSubscriptionAsync(_subscriptionName);
    }

    private void OnThresholdReached(ThresholdReachedEventArgs e)
    {
        var handler = ThresholdReached;
        handler?.Invoke(this, e);
    }
    
    public event ThresholdReachedEventHandler ThresholdReached;
}

public class ThresholdReachedEventArgs : EventArgs
{
    public PubSubMessage Message { get; set; }
}

public delegate void ThresholdReachedEventHandler(object sender, ThresholdReachedEventArgs e);