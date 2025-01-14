namespace Webhooks.Services;

internal class WebhookPayload<T>
{
    public Guid Id { get; set; }
    public string EventType { get; set; } = string.Empty;
    public Guid SubscriptionId { get; set; }
    public DateTime TimeStamp { get; set; }
    public T? Data { get; set; }
}