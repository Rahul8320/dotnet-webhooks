namespace Webhooks.Models;

public sealed record Order(Guid Id, string CustomerName, decimal Amount, DateTime CreateAt);

public sealed record CreateOrderRequest(string CustoemrName, decimal Amount);
