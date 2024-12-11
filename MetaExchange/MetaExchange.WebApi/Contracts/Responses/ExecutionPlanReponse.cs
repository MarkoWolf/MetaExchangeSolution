namespace MetaExchange.WebApi.Contracts.Responses;

public class ExecutionPlanReponse
{
    public string ExchangeId { get; set; } 
    
    public List<PlanOrderResponse> Orders { get; set; } = new(); 
    
    public decimal TotalPrice { get; set; } 
}