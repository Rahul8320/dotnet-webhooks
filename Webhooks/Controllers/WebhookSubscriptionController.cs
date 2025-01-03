using Microsoft.AspNetCore.Mvc;
using Webhooks.Models;
using Webhooks.Repositories;

namespace Webhooks.Controllers;

[Route("api/webhooks/subscriptions")]
[ApiController]
public class WebhookSubscriptionController(
    InMemoryWebhookSubscriptionRepository subscriptionRepository
    ) : ControllerBase
{
    [HttpPost]
    public IResult CreateSubscription([FromBody] CreateWebhookRequest request)
    {
        var subcription = new WebhookSubscription(Guid.NewGuid(), request.EventType, request.WebhookUrl, DateTime.UtcNow);

        subscriptionRepository.Add(subcription);

        return Results.Ok(subcription);
    }
}
