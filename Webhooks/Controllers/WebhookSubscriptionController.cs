using Microsoft.AspNetCore.Mvc;
using Webhooks.Models;
using Webhooks.Repositories;

namespace Webhooks.Controllers;

[Route("api/webhooks/subscriptions")]
[ApiController]
public sealed class WebhookSubscriptionController(
    WebhookSubscriptionRepository subscriptionRepository
    ) : ControllerBase
{
    [HttpPost]
    public async Task<IResult> CreateSubscription([FromBody] CreateWebhookRequest request)
    {
        var subscription = new WebhookSubscription(Guid.NewGuid(), request.EventType, request.WebhookUrl, DateTime.UtcNow);

        await subscriptionRepository.Add(subscription);

        return Results.Ok(subscription);
    }

    [HttpGet]
    public async Task<IResult> GetAllSubscription()
    {
        var subscriptions = await subscriptionRepository.GetAll();

        return Results.Ok(subscriptions);
    }
}
