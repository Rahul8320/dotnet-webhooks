using Microsoft.EntityFrameworkCore;
using Webhooks.Data;
using Webhooks.Models;

namespace Webhooks.Repositories;

public sealed class OrderRepository(WebhooksDbContext dbContext)
{
    public async Task Add(Order order)
    {
        await dbContext.Orders.AddAsync(order);

        await dbContext.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<Order>> GetAll()
    {
        var orders = await dbContext.Orders.AsNoTracking().ToListAsync();

        return orders.AsReadOnly();
    }
}