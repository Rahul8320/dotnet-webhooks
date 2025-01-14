using Microsoft.EntityFrameworkCore;
using Webhooks.Data;

namespace Webhooks.Extensions;

public static class WebApplicationExtensions
{
    public static async Task ApplyMigrationsAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<WebhooksDbContext>();

        await db.Database.MigrateAsync();
    }
}
