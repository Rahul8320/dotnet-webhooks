using Microsoft.EntityFrameworkCore;
using Webhooks.Models;

namespace Webhooks.Data;

internal sealed class WebhooksDbContext(DbContextOptions<WebhooksDbContext> options): DbContext(options)
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<WebhookSubscription> WebhookSubscriptions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(builder =>
        {
            builder.ToTable("orders");
            builder.HasKey(o => o.Id);
        });

        modelBuilder.Entity<WebhookSubscription>(builder => 
        {
            builder.ToTable("subscriptions", "webhooks");
            builder.HasKey(w => w.Id);
        });
    }
}
