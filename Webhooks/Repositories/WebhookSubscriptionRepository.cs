using Microsoft.EntityFrameworkCore;
using Webhooks.Data;
using Webhooks.Models;

namespace Webhooks.Repositories;

public sealed class WebhookSubscriptionRepository(WebhooksDbContext dbContext)
{
    public async Task Add(WebhookSubscription subscription)
    {
        await dbContext.WebhookSubscriptions.AddAsync(subscription);

        await dbContext.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<WebhookSubscription>> GetByEventType(string eventType)
    {
        var events = await dbContext.WebhookSubscriptions
            .AsNoTracking()
            .Where(s => s.EventType == eventType)
            .ToListAsync();

        return events.AsReadOnly();
    }
}