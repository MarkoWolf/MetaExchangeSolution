using MetaExchange.Core.Interfaces;
using MetaExchange.Core.Mappers;
using MetaExchange.Core.Models;

namespace MetaExchange.Core.Services;

public class ExchangeAggregatorService : IExchangeAggregatorService
{
    public AggregatedExchanges GetSortedBids(List<Exchange> exchanges)
    {
        var result = AggregateExchangesWithBids(exchanges);

        result.AggregatedOrders = result.AggregatedOrders
            .OrderByDescending(aggregatedOrder => aggregatedOrder.Order.Price)
            .ToList();

        return result;
    }

    public AggregatedExchanges GetSortedAsks(List<Exchange> exchanges)
    {
        var result = AggregateExchangesWithAsks(exchanges);

        result.AggregatedOrders = result.AggregatedOrders
            .OrderBy(aggregatedOrder => aggregatedOrder.Order.Price)
            .ToList();

        return result;
    }
    private bool IsValidForAsksAggregation(Exchange exchange)
    {
        return exchange.OrderBook.Asks.Count != 0 &&
               exchange.AvailableFunds is { Crypto: > 0 };
    }
    private bool IsValidForBitsAggregation(Exchange exchange)
    {
        return exchange.OrderBook.Bids.Count != 0 &&
               exchange.AvailableFunds is { Euro: > 0 };
    }


    private AggregatedExchanges AggregateExchangesWithAsks(List<Exchange> exchanges)
    {
        var result = new AggregatedExchanges();

        foreach (var exchange in exchanges)
        {
            if (!IsValidForAsksAggregation(exchange))
            {
                continue;
            }

            result.Funds.Add(AggregateFunds(exchange));
            result.AggregatedOrders.AddRange(AggregateOrders(exchange.Id, exchange.OrderBook.Asks));
        }
        return result;
    }
    
    private AggregatedExchanges AggregateExchangesWithBids(List<Exchange> exchanges)
    {
        var result = new AggregatedExchanges();

        foreach (var exchange in exchanges)
        {
            if (!IsValidForBitsAggregation(exchange))
            {
                continue;
            }

            result.Funds.Add(AggregateFunds(exchange));
            result.AggregatedOrders.AddRange(AggregateOrders(exchange.Id, exchange.OrderBook.Bids));
        }
        return result;
    }
    
    private AggregatedFund AggregateFunds(Exchange exchange)
    {
        return new AggregatedFund
        {
            Id = exchange.Id,
            Crypto = exchange.AvailableFunds.Crypto,
            Euro = exchange.AvailableFunds.Euro
        };
    }

    private List<AggregatedExchangeOrder> AggregateOrders(string exchangeId, List<OrderEntry> orderEntries)
    {
        return orderEntries.Where(ask => ask.Order.Amount > 0)
            .Select(ask => new AggregatedExchangeOrder
            {
                ExchangeId = exchangeId,
                Order = ask.Order.Map()
            })
            .ToList();
    }
}