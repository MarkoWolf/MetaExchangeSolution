namespace MetaExchange.Core.Models;

public class Exchange
{
    public string Id { get; set; }  = string.Empty;
    public Funds AvailableFunds { get; set; } = new();
    public OrderBook OrderBook { get; set; } = new();

}