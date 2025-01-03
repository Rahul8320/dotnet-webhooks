using Microsoft.AspNetCore.Mvc;
using Webhooks.Models;
using Webhooks.Repositories;

namespace Webhooks.Controllers;

[Route("api/orders")]
[ApiController]
public class OrderController(InMemoryOrderRepository orderRepository) : ControllerBase
{
    [HttpPost]
    public IResult CreateOrder([FromBody] CreateOrderRequest request)
    {
        var order = new Order(Guid.NewGuid(), request.CustomerName, request.Amount, DateTime.UtcNow);

        orderRepository.Add(order);

        return Results.Ok(order);
    }

    [HttpGet]
    public IResult GetAllOrders()
    {
        var orders = orderRepository.GetAll();

        return Results.Ok(orders);
    }
}
