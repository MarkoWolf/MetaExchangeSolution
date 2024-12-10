namespace MetaExchange.JsonProvider.Models;

public class JsonOrderBook
{
    public List<JsonOrderEntry> Bids { get; set; } = [];
    public List<JsonOrderEntry> Asks { get; set; } = [];
}