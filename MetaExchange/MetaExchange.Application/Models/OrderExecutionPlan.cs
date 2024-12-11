namespace MetaExchange.Application.Models;

public class OrderExecutionPlan
{
    public string ExchangeId { get; set; } 
    
    public List<PlanOrder> Orders { get; set; } = new(); 
    
    public decimal TotalPrice { get; set; } 
}