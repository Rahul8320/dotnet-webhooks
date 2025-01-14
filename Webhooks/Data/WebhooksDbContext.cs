using Microsoft.EntityFrameworkCore;
using Webhooks.Models;

namespace Webhooks.Data;

public sealed class WebhooksDbContext(DbContextOptions<WebhooksDbContext> options): DbContext(options)
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<WebhookSubscription> WebhookSubscriptions { get; set; }
    public DbSet<WebhookDeliveryAttempt> WebhookDeliveryAttempts { get; set; }

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

        modelBuilder.Entity<WebhookDeliveryAttempt>(builder =>
        {
            builder.ToTable("delivery_attempts", "webhooks");
            builder.HasKey(w => w.Id);

            builder.HasOne<WebhookSubscription>()
                .WithMany()
                .HasForeignKey(w => w.WebhookSubscriptionId);
        });
    }
}
