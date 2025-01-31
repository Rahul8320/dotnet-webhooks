using Microsoft.EntityFrameworkCore;
using System.Threading.Channels;
using Webhooks.Data;
using Webhooks.Extensions;
using Webhooks.Repositories;
using Webhooks.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();

builder.Services.AddDbContext<WebhooksDbContext>(option => 
    option.UseSqlite(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<OrderRepository>();
builder.Services.AddScoped<WebhookSubscriptionRepository>();
builder.Services.AddScoped<WebhookDeliveryAttemptRepository>();

builder.Services.AddScoped<WebhookDispatcher>();

builder.Services.AddHostedService<WebhookProcesser>();

builder.Services.AddSingleton(_ =>
{
    return Channel.CreateBounded<WebhookDispatch>(new BoundedChannelOptions(100)
    {
        FullMode = BoundedChannelFullMode.Wait
    });
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    await app.ApplyMigrationsAsync();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
