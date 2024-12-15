using MetaExchange.CLI.Enums;
using MetaExchange.CLI.Interfaces;
using MetaExchange.Core.Extensions;
using MetaExchange.Core.Interfaces;
using MetaExchange.Core.Models;
using Microsoft.Extensions.Logging;

namespace MetaExchange.CLI.Services;

public class OrderHandler(IBuyService buyService, ISellService sellService, ILogger<OrderHandler> logger) : IOrderHandler
{
    private readonly ILogger<OrderHandler> _logger = logger.NotNull(nameof(logger));
    private readonly IBuyService _buyService = buyService.NotNull(nameof(buyService));
    private readonly ISellService _sellService = sellService.NotNull(nameof(sellService));

    public List<OrderExecutionResult> ProcessOrder(OrderType orderType, decimal amount, List<Exchange> exchanges)
    {
        if (exchanges is null || exchanges.Count == 0)
        {
            _logger.LogWarning("No exchanges available to process the order of type {orderType} for amount {amount}.", orderType, amount);
            return [];
        }

        return orderType switch
        {
            OrderType.Buy => _buyService.GetExecuteOrders(amount, exchanges),
            OrderType.Sell => _sellService.GetExecuteOrders(amount, exchanges),
            _ => HandleUnsupportedOrderType(orderType, amount, exchanges)
        };
    }

    private List<OrderExecutionResult> HandleUnsupportedOrderType(OrderType orderType, decimal amount, List<Exchange> exchanges)
    {
        _logger.LogWarning("Unsupported order type: {orderType}. Unable to process the order for amount {amount}. Available exchanges: {count}.", orderType, amount, exchanges.Count);
        return [];
    }

    public List<Exchange> ExecuteOrders(OrderType orderType, List<OrderExecutionResult> results, List<Exchange> exchanges)
    {
        if (results is null || results.Count == 0)
        {
            _logger.LogWarning("No execution results available for order type {orderType}. Available exchanges: {count}.", orderType, exchanges?.Count ?? 0);
            return [];
        }

        return orderType switch
        {
            OrderType.Buy => _buyService.ExecutedOrder(results, exchanges),
            OrderType.Sell => _sellService.ExecutedOrder(results, exchanges),
            _ => HandleUnsupportedOrderType(orderType, exchanges)
        };
    }

    private List<Exchange> HandleUnsupportedOrderType(OrderType orderType, List<Exchange> exchanges)
    {
        _logger.LogWarning("Unsupported order type: {orderType}. Unable to execute orders. Available exchanges: {count}.", orderType, exchanges.Count);
        return [];
    }
}