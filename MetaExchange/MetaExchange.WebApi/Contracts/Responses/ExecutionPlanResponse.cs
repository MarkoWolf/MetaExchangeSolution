namespace MetaExchange.WebApi.Contracts.Responses;

public class ExecutionPlanResponse
{
    public string ExchangeId { get; set; } = string.Empty;
    
    public List<PlanOrderResponse> Orders { get; set; } = new(); 
    
    public decimal TotalPrice { get; set; } 
}