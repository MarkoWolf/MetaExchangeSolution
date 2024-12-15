using MetaExchange.Core.Models;

namespace MetaExchange.Core.Interfaces;

public interface IOrderExecutionService
{
    List<OrderExecutionResult> GetExecuteOrders(decimal amount, List<Exchange> exchanges);
    List<Exchange> ExecutedOrder(List<OrderExecutionResult> orderExecutions, List<Exchange> exchanges);
}