using MetaExchange.Application.Models;

namespace MetaExchange.Application.Interfaces;

public interface IOrderBookService
{
    List<OrderExecutionPlan> GetBuyExecutePlan(decimal amount);
    List<OrderExecutionPlan> GetSellExecutePlan(decimal amount);
}