using Webhooks.Repositories;

namespace Webhooks.Services;

public sealed class WebhookDispatcher(
    HttpClient httpClient, 
    InMemoryWebhookSubscriptionRepository subscriptionRepository)
{
    public async Task DispatchAsync(string eventType, object payload)
    {
        var subscriptions = subscriptionRepository.GetByEventType(eventType);

        foreach (var subscription in subscriptions)
        {
            var request = new
            {
                Id = Guid.NewGuid(),
                subscription.EventType,
                SubscriptionId = subscription.Id,
                TimeStamp = DateTime.UtcNow,
                Data = payload,
            };

            await httpClient.PostAsJsonAsync(subscription.WebhookUrl, request);
        }
    }
}
