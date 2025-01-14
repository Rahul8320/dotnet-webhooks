﻿namespace Webhooks.Models;

public class WebhookDeliveryAttempt
{
    public Guid Id { get; set; }
    public Guid WebhookSubscriptionId { get; set; }
    public string Payload { get; set; } = string.Empty;
    public int? ResponseStatusCode { get; set; }
    public bool Success { get; set; }
    public DateTime Timestamp { get; set; }
}
