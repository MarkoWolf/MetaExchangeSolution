namespace MetaExchange.Core.Models;

public class Order
{
    public Guid Id { get; set; }
    public DateTime Time { get; set; }
    public string Type { get; set; } = String.Empty;
    public string Kind { get; set; } = String.Empty;
    public decimal Amount { get; set; }
    public decimal Price { get; set; }

}