using Webhooks.Data;
using Webhooks.Models;

namespace Webhooks.Repositories;

public class WebhookDeliveryAttemptRepository(WebhooksDbContext dbContext)
{
    public async Task Add(WebhookDeliveryAttempt attempt)
    {
        await dbContext.WebhookDeliveryAttempts.AddAsync(attempt);

        await dbContext.SaveChangesAsync();
    }
}
