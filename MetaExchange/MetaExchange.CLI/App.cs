using MetaExchange.Core.Extensions;
using MetaExchange.Core.Interfaces;
using MetaExchange.Core.Models;
using MetaExchange.JsonProvider.Interfaces;

namespace MetaExchange.CLI;

public class App
{
    private readonly ISellService _sellService;
    private readonly IBuyService _buyService;
    private readonly IReadOnlyExchangeJsonRepository _exchangeJsonRepository;

    public App(IReadOnlyExchangeJsonRepository exchangeJsonRepository, IBuyService buyService, ISellService sellService)
    {
        _sellService = sellService.NotNull(nameof(sellService));
        _buyService = buyService.NotNull(nameof(buyService));
        _exchangeJsonRepository = exchangeJsonRepository.NotNull(nameof(exchangeJsonRepository));
    }

    public async Task RunAsync()
    {
        Console.WriteLine("Welcome to MetaExchange Command Line Interface!");

        Console.WriteLine("Welcome to MetaExchange CLI!");

        List<Exchange> exchanges = _exchangeJsonRepository.GetAll();

        var continueRunning = true;
        while (continueRunning)
        {
            if (exchanges == null || !exchanges.Any())
            {
                Console.WriteLine("No exchanges found. Exiting...");
                continueRunning = false;
            }

            var maxAmountOfBtc = exchanges.Sum(exchange => exchange.AvailableFunds.Crypto);
            var maxAmountOfEuro = exchanges.Sum(exchange => exchange.AvailableFunds.Euro);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"Currentliy you can Buy for {maxAmountOfBtc} BTC");
            Console.WriteLine($"All Exchanegs have  {maxAmountOfEuro:F2} Euro to spend");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Enter order type (Buy/Sell):");
            string? orderTypeInput = Console.ReadLine();
            if (!Enum.TryParse(orderTypeInput, true, out OrderType orderType))
            {
                Console.WriteLine("Invalid order type. Exiting...");
                return;
            }
            
            Console.WriteLine();
            Console.WriteLine();
            
            Console.WriteLine("Enter the amount of BTC:");
            string? amountInput = Console.ReadLine();
            if (!decimal.TryParse(amountInput, out decimal amount) || amount <= 0)
            {
                Console.WriteLine("Invalid amount. Exiting...");
                return;
            }

            List<OrderExecutionResult> executionResults = orderType switch
            {
                OrderType.Buy => _buyService.GetExecuteOrders(amount, exchanges),
                OrderType.Sell => _sellService.GetExecuteOrders(amount, exchanges),
                _ => new List<OrderExecutionResult>()
            };
            
            Console.WriteLine();
            Console.WriteLine();
            
            if (executionResults == null || !executionResults.Any())
            {
                Console.WriteLine("Could not fulfill the order.");
            }
            else
            {
                Console.WriteLine($"Order to {orderType}");
                var totalPrice = executionResults.Sum(executionResult => executionResult.TotalPrice);
              
                foreach (var result in executionResults)
                {
                    Console.WriteLine($"Exchange: {result.ExchangeId}");
                    Console.WriteLine($"Total Price: {result.TotalPrice:F2} EUR");
                    foreach (var order in result.Orders)
                    {
                        Console.WriteLine($"- Order ID: {order.OrderId}, Amount: {order.Amount}, TotalPrice: {order.TotalPrice:F2} EUR, Price: {order.Price:F2} EUR");
                    }
                }
                
                Console.WriteLine();
                
                if (orderType == OrderType.Buy)
                {
                    Console.WriteLine($"Buy {amountInput} BTC for a Total price: {totalPrice:F2} Euro");
                }
                else
                {
                    Console.WriteLine($"Sell {amountInput} BTC for a Total price: {totalPrice:F2} Euro");
                }
        
                
                Console.WriteLine();
                Console.WriteLine();
                
                Console.WriteLine("Do you want to execute this order? (yes/no)");
                string? placeTheOrder = Console.ReadLine();
                if (!Enum.TryParse(placeTheOrder, true, out Decision placeTheOrderDecision))
                {
                    Console.WriteLine("Invalid order type. Exiting...");
                    return;
                }
                
                Console.WriteLine();
                Console.WriteLine();
                
                if (placeTheOrderDecision == Decision.Yes)
                {
                    exchanges = orderType switch
                    {
                        OrderType.Buy => _buyService.ExecutedOrder(executionResults, exchanges),
                        OrderType.Sell => _sellService.ExecutedOrder(executionResults, exchanges),
                        _ => new List<Exchange>()
                    };
                }
            }

            Console.WriteLine("Would you like to perform another action? (yes/no)");
            string? performAnotherAction = Console.ReadLine();
            if (!Enum.TryParse(performAnotherAction, true, out Decision performAnotherActionDecision))
            {
                Console.WriteLine("Invalid order type. Exiting...");
                return;
            }
            
            Console.WriteLine();
            Console.WriteLine();
            
            if (performAnotherActionDecision == Decision.No)
            {
                continueRunning = false;
            }
        }
    }
}