using MetaExchange.CLI.Enums;
using MetaExchange.Core.Models;

namespace MetaExchange.CLI.Interfaces;

public interface IUserInteractionService
{
    OrderType GetOrderType();
    decimal GetAmount();
    void DisplayExecutionResults(OrderType orderType, decimal amount, List<OrderExecutionResult> results);
    bool ConfirmExecution();
    bool ConfirmAnotherAction();
    void DisplayExchangeSummary(List<Exchange> exchanges);
}