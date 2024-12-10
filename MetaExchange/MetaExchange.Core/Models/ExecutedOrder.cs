namespace MetaExchange.Core.Models;

public class ExecutedOrder
{
    public Guid OrderId { get; set; } 
    public decimal Amount { get; set; } 
    public decimal Price { get; set; } 
    public decimal TotalPrice { get; set; }
}