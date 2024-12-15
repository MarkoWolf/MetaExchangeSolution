namespace MetaExchange.JsonProvider.Models;

public class JsonOrder
{
    public Guid Id { get; set; }
    public DateTime Time { get; set; }
    public string Type { get; set; } = String.Empty;
    public string Kind { get; set; } = String.Empty;
    public decimal Amount { get; set; }
    public decimal Price { get; set; }
}