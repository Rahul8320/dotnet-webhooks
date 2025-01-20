namespace Webhooks.Services;

public sealed record WebhookDispatch(string EvenType, object Data);
