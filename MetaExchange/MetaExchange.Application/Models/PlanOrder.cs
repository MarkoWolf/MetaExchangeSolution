namespace MetaExchange.Application.Models;

public class PlanOrder
{
    public Guid OrderId { get; set; } 
    public decimal Amount { get; set; } 
    public decimal Price { get; set; } 
    public decimal TotalPrice { get; set; }
}