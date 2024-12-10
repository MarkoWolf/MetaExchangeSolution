namespace MetaExchange.Core.Models;

public class OrderBook
{
    public List<OrderEntry> Bids { get; set; } = [];
    public List<OrderEntry> Asks { get; set; } = [];
}