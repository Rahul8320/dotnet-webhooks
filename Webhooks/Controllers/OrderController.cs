using Microsoft.AspNetCore.Mvc;
using Webhooks.Models;
using Webhooks.Repositories;
using Webhooks.Services;

namespace Webhooks.Controllers;

[Route("api/orders")]
[ApiController]
public class OrderController(
    OrderRepository orderRepository,
    WebhookDispatcher webhookDispatcher) : ControllerBase
{
    [HttpPost]
    public async Task<IResult> CreateOrder([FromBody] CreateOrderRequest request)
    {
        var order = new Order(Guid.NewGuid(), request.CustomerName, request.Amount, DateTime.UtcNow);

        await orderRepository.Add(order);

        await webhookDispatcher.DispatchAsync("order.created", order);

        return Results.Ok(order);
    }

    [HttpGet]
    public async Task<IResult> GetAllOrders()
    {
        var orders = await orderRepository.GetAll();

        return Results.Ok(orders);
    }
}
