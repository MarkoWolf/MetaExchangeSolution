using MetaExchange.Core.Models;

namespace MetaExchange.Core.Services;

public class BaseOrderExecutionService
{
    protected bool IsValidInput(decimal amount, List<Exchange> exchanges)
    {
        return amount > 0 && exchanges != null && exchanges.Any();
    }

    protected void AddOrUpdateExecutionResult(List<OrderExecutionResult> result, AggregatedExchangeOrder aggregatedExchangeOrder, decimal amount)
    {
        decimal totalPrice = amount * aggregatedExchangeOrder.Order.Price;

        var executedOrder = new ExecutedOrder
        {
            OrderId = aggregatedExchangeOrder.Order.Id,
            Amount = amount,
            Price = aggregatedExchangeOrder.Order.Price,
            TotalPrice = totalPrice
        };

        OrderExecutionResult? executionResult = result.FirstOrDefault(r => r.ExchangeId == aggregatedExchangeOrder.ExchangeId);

        if (executionResult != null)
        {
            executionResult.Orders.Add(executedOrder);
            executionResult.TotalPrice += totalPrice;
        }
        else
        {
            result.Add(new OrderExecutionResult
            {
                ExchangeId = aggregatedExchangeOrder.ExchangeId,
                Orders = [executedOrder],
                TotalPrice = totalPrice
            });
        }
    }
}