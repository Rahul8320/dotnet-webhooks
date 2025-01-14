using Microsoft.AspNetCore.Mvc;
using Webhooks.Models;
using Webhooks.Repositories;

namespace Webhooks.Controllers;

[Route("api/webhooks/subscriptions")]
[ApiController]
public class WebhookSubscriptionController(
    WebhookSubscriptionRepository subscriptionRepository
    ) : ControllerBase
{
    [HttpPost]
    public async Task<IResult> CreateSubscription([FromBody] CreateWebhookRequest request)
    {
        var subcription = new WebhookSubscription(Guid.NewGuid(), request.EventType, request.WebhookUrl, DateTime.UtcNow);

        await subscriptionRepository.Add(subcription);

        return Results.Ok(subcription);
    }
}
