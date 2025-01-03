﻿using Webhooks.Models;

namespace Webhooks.Repositories;

internal sealed class InMemoryWebhookSubscriptionRepository
{
    private readonly List<WebhookSubscription> _subscriptions = [];

    public void Add(WebhookSubscription subscription)
    {
        _subscriptions.Add(subscription);
    }

    public IReadOnlyList<WebhookSubscription> GetByEventType(string eventType)
    {
        return _subscriptions.Where(s => s.EventType == eventType).ToList().AsReadOnly();
    }
}
