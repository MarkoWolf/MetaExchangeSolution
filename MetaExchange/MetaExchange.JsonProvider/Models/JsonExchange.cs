namespace MetaExchange.JsonProvider.Models;

public class JsonExchange
{
    public string Id { get; set; } = string.Empty;
    public JsonFunds AvailableFunds { get; set; } = new();
    public JsonOrderBook OrderBook { get; set; } = new();
}