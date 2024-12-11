using MetaExchange.CLI.Enums;
using MetaExchange.Core.Models;

namespace MetaExchange.CLI.Interfaces;

public interface IOrderHandler
{
    List<OrderExecutionResult> ProcessOrder(OrderType orderType, decimal amount, List<Exchange> exchanges);
    List<Exchange> ExecuteOrders(OrderType orderType, List<OrderExecutionResult> results, List<Exchange> exchanges);
}