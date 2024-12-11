using MetaExchange.Core.Extensions;
using MetaExchange.Core.Interfaces;
using MetaExchange.Core.Models;

namespace MetaExchange.Core.Services;

public class BuyService(IExchangeAggregatorService asksExchangeAggregatorService) : BaseOrderExecutionService, IBuyService
{
    private readonly IAsksExchangeAggregatorService _asksExchangeAggregatorService = asksExchangeAggregatorService.NotNull(nameof(asksExchangeAggregatorService));

    public List<OrderExecutionResult> GetExecuteOrders(decimal amount, List<Exchange> exchanges)
    {
        if (!IsValidInput(amount, exchanges))
        {
            return new List<OrderExecutionResult>();
        }

        var aggregatedExchanges = _asksExchangeAggregatorService.GetSortedAsks(exchanges);

        var result = GetOrderExecutionResults(aggregatedExchanges, amount);

        return result;
    }

    public List<Exchange> ExecutedOrder(List<OrderExecutionResult> orderExecutions, List<Exchange> exchanges)
    {
        foreach (OrderExecutionResult orderExecution in orderExecutions)
        {
            Exchange? exchange = exchanges.FirstOrDefault(exchange => exchange.Id == orderExecution.ExchangeId);
            if (exchange == null)
            {
                return [];
            }

            foreach (var order in orderExecution.Orders)
            {
                OrderEntry? ask = exchange.OrderBook.Asks.FirstOrDefault(ask => ask.Order.Id == order.OrderId);
                if (ask is null)
                {
                    return [];
                }
                ask.Order.Amount -= order.Amount;
                exchange.AvailableFunds.Crypto -= order.Amount;
                exchange.AvailableFunds.Euro += order.TotalPrice;
            }
        }
        return exchanges;
    }

    private List<OrderExecutionResult> GetOrderExecutionResults(AggregatedExchanges aggregatedExchanges, decimal amount)
    {
        var remainingAmount = amount;
        var result = new List<OrderExecutionResult>();
        foreach (AggregatedExchangeOrder aggregatedOrder in aggregatedExchanges.AggregatedOrders)
        {
            if (remainingAmount == 0)
            {
                break;
            }

            AggregatedFund? exchange = aggregatedExchanges.Funds.FirstOrDefault(fund => fund.Id == aggregatedOrder.ExchangeId);

            if (exchange is null)
            {
                continue;
            }

            decimal possibleAmountToBuy = CalculatePossibleAmountToBuy(remainingAmount, aggregatedOrder.Order.Amount, exchange.Crypto);

            remainingAmount = ProcessOrderAndUpdateFunds(aggregatedOrder.Order, exchange, possibleAmountToBuy, remainingAmount);

            AddOrUpdateExecutionResult(result, aggregatedOrder, possibleAmountToBuy);
        }

        return remainingAmount > 0 ? new List<OrderExecutionResult>() : result;
    }

    private decimal ProcessOrderAndUpdateFunds(AggregatedOrder order, AggregatedFund exchange, decimal amountToBuy, decimal remainingAmount)
    {
        order.Amount -= amountToBuy;
        exchange.Crypto -= amountToBuy;
        return remainingAmount - amountToBuy;
    }

    private decimal CalculatePossibleAmountToBuy(decimal remainingAmount, decimal orderAmount, decimal exchangeCrypto)
    {
        return Math.Min(Math.Min(remainingAmount, orderAmount), exchangeCrypto);
    }
}