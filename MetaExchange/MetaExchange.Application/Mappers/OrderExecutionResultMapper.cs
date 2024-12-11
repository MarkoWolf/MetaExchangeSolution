using MetaExchange.Application.Models;
using MetaExchange.Core.Models;

namespace MetaExchange.Application.Mappers;

public static class OrderExecutionResultMapper
{
    public static List<OrderExecutionPlan> Map(this List<OrderExecutionResult> results)
    {
        return results.Select(result => result.Map()).ToList();
    }
    
    private static OrderExecutionPlan Map(this OrderExecutionResult result)
    {
        return new OrderExecutionPlan
        {
            ExchangeId = result.ExchangeId,
            TotalPrice = result.TotalPrice,
            Orders = result.Orders.Select(order => new PlanOrder
            {
                OrderId = order.OrderId,
                Amount = order.Amount,
                Price = order.Price,
                TotalPrice = order.TotalPrice
            }).ToList()
        };
    }
    

}