using MetaExchange.Core.Models;

namespace MetaExchange.Core.Extensions;

public static class OrderMapper
{
    public static AggregatedOrder MapOrderToAggregatedOrder(this Order order)
    {
        return new AggregatedOrder
        {
            Id = order.Id, 
            Amount = order.Amount, 
            Price = order.Price
        };
    }
}