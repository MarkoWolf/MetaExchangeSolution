using MetaExchange.Core.Extensions;
using MetaExchange.Core.Interfaces;
using MetaExchange.Core.Models;

namespace MetaExchange.Core.Services;

public class SellService(IExchangeAggregatorService bitsExchangeAggregatorService) : BaseOrderExecutionService, ISellService
{
    private readonly IBitsExchangeAggregatorService _bitsExchangeAggregatorService = bitsExchangeAggregatorService.NotNull(nameof(bitsExchangeAggregatorService));

    public List<OrderExecutionResult> GetExecuteOrders(decimal amount, List<Exchange> exchanges)
    {
        if (!IsValidInput(amount, exchanges))
        {
            return new List<OrderExecutionResult>();
        }

        AggregatedExchanges aggregatedExchanges = _bitsExchangeAggregatorService.GetSortedBids(exchanges);

        List<OrderExecutionResult> result = GetOrderExecutionResults(aggregatedExchanges, amount);

        return result;
    }

    public List<Exchange> ExecutedOrder(List<OrderExecutionResult> orderExecutions, List<Exchange> exchanges)
    {
        foreach (var orderExecution in orderExecutions)
        {
            Exchange? exchange = exchanges.FirstOrDefault(exchange => exchange.Id == orderExecution.ExchangeId);
            if (exchange == null)
            {
                return [];
            }

            foreach (ExecutedOrder order in orderExecution.Orders)
            {
                OrderEntry? bid = exchange.OrderBook.Bids.FirstOrDefault(bid => bid.Order.Id == order.OrderId);
                if (!IsOrderValid(bid, order, exchange))
                {
                    return []; 
                }
                bid.Order.Amount -= order.Amount;
                exchange.AvailableFunds.Crypto += order.Amount;
                exchange.AvailableFunds.Euro -= order.TotalPrice;
            }
        }

        return exchanges;
    }

    private static bool IsOrderValid(OrderEntry? bid, ExecutedOrder order, Exchange exchange)
    {
        return bid is not null &&  order.Amount < bid.Order.Amount && order.TotalPrice < exchange.AvailableFunds.Euro;
    }

    private List<OrderExecutionResult> GetOrderExecutionResults(AggregatedExchanges aggregatedExchanges, decimal amount)
    {
        decimal remainingAmount = amount;
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

            decimal possibleAmountToSell = CalculatePossibleAmountToSell(remainingAmount, aggregatedOrder.Order, exchange);

            remainingAmount = ProcessOrderAndUpdateFunds(aggregatedOrder.Order, exchange, possibleAmountToSell, remainingAmount);

            AddOrUpdateExecutionResult(result, aggregatedOrder, possibleAmountToSell);
        }

        return remainingAmount > 0 ? new List<OrderExecutionResult>() : result;
    }

    private decimal ProcessOrderAndUpdateFunds(AggregatedOrder order, AggregatedFund exchange, decimal amountToBuy, decimal remainingAmount)
    {
        order.Amount -= amountToBuy;
        exchange.Euro -= amountToBuy * order.Price;
        return remainingAmount - amountToBuy;
    }

    private decimal CalculatePossibleAmountToSell(decimal remainingAmount, AggregatedOrder order, AggregatedFund exchange)
    {
        if (order.Price == 0)
        {
            return 0;
        }

        decimal maxExchangeAmountToSell = exchange.Euro / order.Price;

        decimal possibleAmountToSell = Math.Min(Math.Min(remainingAmount, order.Amount), maxExchangeAmountToSell);

        return possibleAmountToSell;
    }
}