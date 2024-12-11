using MetaExchange.Core.Models;

namespace MetaExchange.Core.Mappers;

public static class OrderMapper
{
    public static AggregatedOrder Map(this Order order)
    {
        return new AggregatedOrder
        {
            Id = order.Id, 
            Amount = order.Amount, 
            Price = order.Price
        };
    }
}