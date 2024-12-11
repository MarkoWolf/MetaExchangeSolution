using MetaExchange.CLI.Enums;
using MetaExchange.CLI.Interfaces;
using MetaExchange.Core.Extensions;
using MetaExchange.JsonProvider.Interfaces;

namespace MetaExchange.CLI;

public class App(IReadOnlyExchangeJsonRepository exchangeJsonRepository, IUserInteractionService userInteractionService, IOrderHandler orderHandler)
{
    private readonly IOrderHandler _orderHandler = orderHandler.NotNull(nameof(orderHandler));
    private readonly IUserInteractionService _userInteractionService = userInteractionService.NotNull(nameof(userInteractionService));
    private readonly IReadOnlyExchangeJsonRepository _exchangeJsonRepository = exchangeJsonRepository.NotNull(nameof(exchangeJsonRepository));

    public async Task RunAsync()
    {
        Console.WriteLine("Welcome to MetaExchange Command Line Interface!");
        var exchanges = _exchangeJsonRepository.GetAll();

        if (exchanges == null || exchanges.Count == 0)
        {
            Console.WriteLine("No exchanges found. Exiting...");
            return;
        }

        while (true)
        {
            _userInteractionService.DisplayExchangeSummary(exchanges);

            var orderType = _userInteractionService.GetOrderType();

            if (orderType == OrderType.NoOrder)
            {
                break;
            }
            
            var amount = _userInteractionService.GetAmount();
            
            if (amount == 0)
            {
                break;
            }
            
            var executionResults = _orderHandler.ProcessOrder(orderType, amount, exchanges);

            if (executionResults == null || executionResults.Count == 0)
            {
                Console.WriteLine("Could not fulfill the order.");
            }
            else
            {
                _userInteractionService.DisplayExecutionResults(orderType, amount, executionResults);

                if (_userInteractionService.ConfirmExecution())
                {
                    exchanges = _orderHandler.ExecuteOrders(orderType, executionResults, exchanges);
                }
            }

            if (!_userInteractionService.ConfirmAnotherAction())
            {
                break;
            }
        }
    }
}