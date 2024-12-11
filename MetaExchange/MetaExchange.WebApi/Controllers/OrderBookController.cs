using MetaExchange.Application.Interfaces;
using MetaExchange.Application.Models;
using MetaExchange.Core.Extensions;
using MetaExchange.WebApi.Contracts.Requests;
using MetaExchange.WebApi.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace MetaExchange.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderBookController(IOrderBookService orderBookService) : ControllerBase
{
    private readonly IOrderBookService _orderBookService = orderBookService.NotNull(nameof(orderBookService));

    [HttpGet]
    [Route("executionplans/buy")]
    public IActionResult GetBuyExecutionPlans([FromQuery] ExecutionPlanRequest request)
    {
        List<OrderExecutionPlan> result = _orderBookService.GetBuyExecutePlan(request.Amount);
        return Ok(result.Map());
    }
    
    [HttpGet]
    [Route("executionplans/sell")]
    public IActionResult GetSellExecutionPlans([FromQuery] ExecutionPlanRequest request)
    {
        List<OrderExecutionPlan> result = _orderBookService.GetSellExecutePlan(request.Amount);
        return Ok(result.Map());
    }
}