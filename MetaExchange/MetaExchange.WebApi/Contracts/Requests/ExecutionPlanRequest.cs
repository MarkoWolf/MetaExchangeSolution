using Microsoft.AspNetCore.Mvc;

namespace MetaExchange.WebApi.Contracts.Requests;

public class ExecutionPlanRequest
{
    [FromQuery(Name = "amount")]
    public decimal Amount { get; set; }
}