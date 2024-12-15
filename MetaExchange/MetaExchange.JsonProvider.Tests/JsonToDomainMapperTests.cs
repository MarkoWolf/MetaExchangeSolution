using FluentAssertions;
using MetaExchange.Core.Models;
using MetaExchange.JsonProvider.Mappers;
using MetaExchange.JsonProvider.Models;

namespace MetaExchange.JsonProvider.Tests;

public class JsonToDomainMapperTests
{
    [Fact]
    public void MapToDomain_ShouldMapJsonExchangeToExchange()
    {
        // Arrange
        var buyGuid = new Guid("a56ef59d-c75d-491f-a972-7ea302894ed4");
        var sellGuid = new Guid("86b69db0-b1cb-49d5-beb7-7068cfcbe14d");
        const string buyType = "Buy";
        const string sellType = "Sell";
        const string limit = "Limit";
        const string exchangeId = "exchange-01";

        var jsonExchange = new JsonExchange
        {
            Id = exchangeId,
            AvailableFunds = new JsonFunds { Crypto = 10.5m, Euro = 1000.75m },
            OrderBook = new JsonOrderBook
            {
                Bids = new List<JsonOrderEntry>
                {
                    new()
                    {
                        Order = new JsonOrder
                        {
                            Id = buyGuid,
                            Time = DateTime.UtcNow,
                            Type = buyType,
                            Kind = limit,
                            Amount = 0.5m,
                            Price = 30000.25m
                        }
                    }
                },
                Asks = new List<JsonOrderEntry>
                {
                    new()
                    {
                        Order = new JsonOrder
                        {
                            Id = sellGuid,
                            Time = DateTime.UtcNow,
                            Type = sellType,
                            Kind = limit,
                            Amount = 1.2m,
                            Price = 31000.75m
                        }
                    }
                }
            }
        };

        // Act
        var result = jsonExchange.Map();

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(exchangeId);
        result.AvailableFunds.Crypto.Should().Be(10.5m);
        result.AvailableFunds.Euro.Should().Be(1000.75m);

        result.OrderBook.Bids.Should().HaveCount(1)
            .And.ContainSingle(bid => bid.Order.Id == buyGuid && bid.Order.Price == 30000.25m);
        result.OrderBook.Asks.Should().HaveCount(1)
            .And.ContainSingle(ask => ask.Order.Id == sellGuid && ask.Order.Price == 31000.75m);
    }

    [Fact]
    public void MapToDomain_ShouldHandleEmptyOrderBook()
    {
        // Arrange
        const string exchangeId = "exchange-02";

        var jsonExchange = new JsonExchange
        {
            Id = exchangeId,
            AvailableFunds = new JsonFunds { Crypto = 5.0m, Euro = 500.0m },
            OrderBook = new JsonOrderBook
            {
                Bids = new List<JsonOrderEntry>(),
                Asks = new List<JsonOrderEntry>()
            }
        };

        // Act
        var result = jsonExchange.Map();

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(exchangeId);
        result.AvailableFunds.Crypto.Should().Be(5.0m);
        result.AvailableFunds.Euro.Should().Be(500.0m);
        result.OrderBook.Bids.Should().BeEmpty();
        result.OrderBook.Asks.Should().BeEmpty();
    }

    [Fact]
    public void MapToDomain_ShouldNull_WhenJsonExchangeIsNull()
    {
        // Arrange
        JsonExchange? jsonExchange = null;

        // Act
        // ! because of testing purpes 
        Exchange result = jsonExchange!.Map();

        // Assert
        result.Should().NotBeNull();
    }
}