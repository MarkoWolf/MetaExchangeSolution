namespace MetaExchange.Core.Models;

public class OrderExecutionResult
{
    public string ExchangeId { get; set; } = string.Empty; 
    public List<ExecutedOrder> Orders { get; set; } = new(); 
    public decimal TotalPrice { get; set; } 
}