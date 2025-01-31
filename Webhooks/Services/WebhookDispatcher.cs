﻿using System.Text.Json;
using System.Threading.Channels;
using Webhooks.Models;
using Webhooks.Repositories;

namespace Webhooks.Services;

public sealed class WebhookDispatcher(
    Channel<WebhookDispatch> webhookChannel,
    IHttpClientFactory httpClientFactory, 
    WebhookSubscriptionRepository subscriptionRepository,
    WebhookDeliveryAttemptRepository deliveryAttemptRepository)
{
    public async Task DispatchAsync<T>(string eventType, T data) where T : notnull
    { 
        await webhookChannel.Writer.WriteAsync(new WebhookDispatch(eventType, data));
    }

    public async Task ProcessAsync<T>(string eventType, T data)
    {
        var subscriptions = await subscriptionRepository.GetByEventType(eventType);

        foreach (WebhookSubscription subscription in subscriptions)
        {
            using HttpClient httpClient = httpClientFactory.CreateClient();

            var payload = new WebhookPayload<T>
            {
                Id = Guid.NewGuid(),
                EventType = subscription.EventType,
                SubscriptionId = subscription.Id,
                TimeStamp = DateTime.UtcNow,
                Data = data
            };

            var jsonPayload = JsonSerializer.Serialize(payload);

            try
            {
                var response = await httpClient.PostAsJsonAsync(subscription.WebhookUrl, payload);

                var attempt = new WebhookDeliveryAttempt
                {
                    Id = Guid.NewGuid(),
                    WebhookSubscriptionId = subscription.Id,
                    Payload = jsonPayload,
                    ResponseStatusCode = (int)response.StatusCode,
                    Success = true,
                    Timestamp = DateTime.UtcNow
                };

                await deliveryAttemptRepository.Add(attempt);
            }
            catch (Exception)
            {
                var attempt = new WebhookDeliveryAttempt
                {
                    Id = Guid.NewGuid(),
                    WebhookSubscriptionId = subscription.Id,
                    Payload = jsonPayload,
                    ResponseStatusCode = null,
                    Success = false,
                    Timestamp = DateTime.UtcNow
                };

                await deliveryAttemptRepository.Add(attempt);
            }

        }
    }
}
