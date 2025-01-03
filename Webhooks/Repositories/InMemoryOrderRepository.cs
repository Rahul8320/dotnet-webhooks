using Webhooks.Models;

namespace Webhooks.Repositories;

public sealed class InMemoryOrderRepository
{
    private readonly List<Order> _orders = [];

    public void Add(Order order)
    {
        _orders.Add(order);
    }

    public IReadOnlyList<Order> GetAll()
    {
        return _orders.AsReadOnly();
    }
}
