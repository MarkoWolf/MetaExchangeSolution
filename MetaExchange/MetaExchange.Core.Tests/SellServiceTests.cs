using FluentAssertions;
using MetaExchange.Core.Models;
using MetaExchange.Core.Services;
using MetaExchange.Core.Tests.TestData;
using MetaExchange.JsonProvider.Configurations;
using MetaExchange.JsonProvider.Repositories;
using MetaExchange.JsonProvider.Services;
using Microsoft.Extensions.Options;

namespace MetaExchange.Core.Tests;

public class SellServiceTests
{
    [Fact]
    public void Execute_ShouldReturnCorrectOrders_WhenValidInput()
    {
        // Arrange
        var exchanges = ExchangeTestData.GetExchanges();

        var sellService = new SellService(new ExchangeAggregatorService());

        decimal amountToSell = 1.5m;

        // Act
        var result = sellService.GetExecuteOrders(amountToSell, exchanges);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCountGreaterThan(0);

        var totalAmount = result.Sum(r => r.Orders.Sum(o => o.Amount));
        totalAmount.Should().Be(amountToSell);

        var totalCost = result.Sum(r => r.TotalPrice);
        totalCost.Should().BeGreaterThan(0);

        result.All(r => r.Orders.All(o => o.Price > 0)).Should().BeTrue();
    }
    
    [Fact]
    public void Execute_ShouldReturnEmptyOrders_WhenInValidInput()
    {
        // Arrange
        var exchanges = ExchangeTestData.GetExchanges();

        var sellService = new SellService(new ExchangeAggregatorService());

        decimal amountToSell = -1.5m;

        // Act
        var result = sellService.GetExecuteOrders(amountToSell, exchanges);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }
    
    [Fact]
    public void Execute_ShouldReturnEmptyOrders_WhenInsufficientFunds()
    {
        // Arrange
        var exchanges = ExchangeTestData.GetExchanges();

        var sellService = new SellService(new ExchangeAggregatorService());

        decimal amountToSell = 1000m; 

        // Act
        var result = sellService.GetExecuteOrders(amountToSell, exchanges);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }
    
    [Fact]
    public void ExecutedOrder_ShouldReturnEmptyList_WhenExchangeNotFound()
    {
        var exchanges = ExchangeTestData.GetExchanges();
        
        // Arrange
        var orderExecutionResults = new List<OrderExecutionResult>
        {
            new OrderExecutionResult
            {
                ExchangeId = "NonExistentExchangeId",
                Orders = new List<ExecutedOrder>
                {
                    new ExecutedOrder
                    {
                        OrderId = Guid.NewGuid(),
                        Amount = 1m,
                        TotalPrice = 50000m
                    }
                }
            }
        };

        var sellService = new SellService(new ExchangeAggregatorService());

        // Act
        var updatedExchanges = sellService.ExecutedOrder(orderExecutionResults, exchanges);

        // Assert
        updatedExchanges.Should().BeEmpty();
    }
}