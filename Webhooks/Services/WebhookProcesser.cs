using System.Threading.Channels;

namespace Webhooks.Services;

public sealed class WebhookProcesser(
    IServiceScopeFactory serviceScopeFactory,
    Channel<WebhookDispatch> webhookChannel) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var events = webhookChannel.Reader.ReadAllAsync(stoppingToken);

        await foreach (WebhookDispatch dispatch in events)
        {
            using IServiceScope scope = serviceScopeFactory.CreateScope();
            var dispatcher = scope.ServiceProvider.GetRequiredService<WebhookDispatcher>();
            await dispatcher.ProcessAsync(dispatch.EvenType, dispatch.Data);
        }
    }
}
