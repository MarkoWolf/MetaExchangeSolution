using MetaExchange.CLI.Enums;
using MetaExchange.CLI.Interfaces;
using MetaExchange.Core.Models;

namespace MetaExchange.CLI.Services;

public class UserInteractionService : IUserInteractionService
{
    public OrderType GetOrderType()
    {
        while (true)
        {    
            Console.WriteLine("Enter order type (Buy/Sell):");
            string? input = Console.ReadLine();
            if (Enum.TryParse(input, true, out OrderType orderType))
            {
                return orderType;
            }

            if (!TryAgain(input))
            {
                return OrderType.NoOrder;
            }
        }
    }

    private bool TryAgain(string? input)
    {
        Console.WriteLine($"The input '{input}' is invalid. Would you like to try again? (yes/no)", input);
        return Console.ReadLine()?.Trim().ToLower() == "yes";
    }

    public decimal GetAmount()
    {
        while (true)
        {
            Console.WriteLine("Enter the amount of BTC (must be greater than zero):");
            string? input = Console.ReadLine();
            if (decimal.TryParse(input, out var amount) && amount > 0)
            {
                return amount;
            }

            if (!TryAgain(input))
            {
                return 0;
            }
        }
    }

    public void DisplayExecutionResults(OrderType orderType, decimal amount, List<OrderExecutionResult> results)
    {
        Console.WriteLine($"Order to {orderType}");
        var totalPrice = results.Sum(executionResult => executionResult.TotalPrice);
        
        foreach (var result in results)
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
            Console.WriteLine($"Buy {amount} BTC for a Total price: {totalPrice:F2} Euro");
        }
        else
        {
            Console.WriteLine($"Sell {amount} BTC for a Total price: {totalPrice:F2} Euro");
        }
    }

    public bool ConfirmExecution()
    {
        Console.WriteLine("Do you want to execute this order? (yes/no)");
        return Console.ReadLine()?.Trim().ToLower() == "yes";
    }

    public bool ConfirmAnotherAction()
    {
        Console.WriteLine("Would you like to perform another action? (yes/no)");
        return Console.ReadLine()?.Trim().ToLower() == "yes";
    }

    public void DisplayExchangeSummary(List<Exchange> exchanges)
    {
        var maxAmountOfBtc = exchanges.Sum(exchange => exchange.AvailableFunds.Crypto);
        var maxAmountOfEuro = exchanges.Sum(exchange => exchange.AvailableFunds.Euro);
        Console.WriteLine($"Currentliy you can Buy for {maxAmountOfBtc} BTC");
        Console.WriteLine($"All Exchanegs have  {maxAmountOfEuro:F2} Euro to spend");
    }
}