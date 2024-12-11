using MetaExchange.Core.Models;
using MetaExchange.JsonProvider.Models;

namespace MetaExchange.JsonProvider.Mappers;

public static class JsonToDomainMapper
{
    public static Exchange Map(this JsonExchange jsonExchange)
    {
        if (jsonExchange == null)
            return new NullExchange();
        
        var domain = new Exchange
        {
            Id = jsonExchange.Id,
            AvailableFunds = new Funds
            {
                Crypto = jsonExchange.AvailableFunds.Crypto,
                Euro = jsonExchange.AvailableFunds.Euro
            },
            OrderBook = new OrderBook
            {
                Bids = jsonExchange.OrderBook.Bids.Select(jsonOrderEntry => jsonOrderEntry.MapToDomainOrder()).ToList(),
                Asks = jsonExchange.OrderBook.Asks.Select(jsonOrderEntry => jsonOrderEntry.MapToDomainOrder()).ToList()
            }
        };
        return domain;
    }
    
    private static OrderEntry MapToDomainOrder(this JsonOrderEntry jsonOrderEntry)
    {
        return new OrderEntry
        {
            Order = new Order {
                Id = jsonOrderEntry.Order.Id,
                Time = jsonOrderEntry.Order.Time,
                Type = jsonOrderEntry.Order.Type,
                Kind = jsonOrderEntry.Order.Kind,
                Amount = jsonOrderEntry.Order.Amount,
                Price = jsonOrderEntry.Order.Price
            }
        };
    }
}
