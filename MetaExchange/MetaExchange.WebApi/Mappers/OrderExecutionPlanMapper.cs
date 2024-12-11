﻿using MetaExchange.Application.Models;
using MetaExchange.WebApi.Contracts.Responses;

namespace MetaExchange.WebApi.Mappers;

public static class OrderExecutionPlanMapper
{
    public static ExecutionPlanReponse Map(this OrderExecutionPlan result)
    {
        return new ExecutionPlanReponse
        {
            ExchangeId = result.ExchangeId,
            TotalPrice = result.TotalPrice,
            Orders = result.Orders.Select(order => new PlanOrderResponse
            {
                OrderId = order.OrderId,
                Amount = order.Amount,
                Price = order.Price,
                TotalPrice = order.TotalPrice
            }).ToList()
        };
    }
    
    public static List<ExecutionPlanReponse> Map(this List<OrderExecutionPlan> results)
    {
        return results.Select(result => result.Map()).ToList();
    }
}