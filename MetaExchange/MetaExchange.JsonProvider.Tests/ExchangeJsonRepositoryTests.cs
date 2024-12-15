using MetaExchange.JsonProvider.Interfaces;
using MetaExchange.JsonProvider.Models;
using MetaExchange.JsonProvider.Repositories;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace MetaExchange.JsonProvider.Tests;

public class ExchangeJsonRepositoryTests
{
    [Fact]
    public void GetAll_ShouldReturnMappedExchanges_WhenJsonExchangesAreValid()
    {
        // Arrange
        var fileService = Substitute.For<IReadOnlyFileService>();
        var logger = Substitute.For<ILogger<ExchangeJsonRepository>>();
        var repository = new ExchangeJsonRepository(fileService, logger);

        var mockJsonExchanges = new List<JsonExchange>
        {
            new JsonExchange { Id = "exchange-01", AvailableFunds = new JsonFunds { Crypto = 10, Euro = 1000 } },
            new JsonExchange { Id = "exchange-02", AvailableFunds = new JsonFunds { Crypto = 20, Euro = 2000 } }
        };

        fileService.ReadAllFromFolder<JsonExchange>().Returns(mockJsonExchanges);

        // Act
        var result = repository.GetAll();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Contains(result, exchange => exchange is { Id: "exchange-01", AvailableFunds.Crypto: 10 });
        Assert.Contains(result, exchange => exchange is { Id: "exchange-02", AvailableFunds.Crypto: 20 });
    }
    
    [Fact]
    public void GetAll_ShouldReturnEmptyList_WhenNoJsonExchangesExist()
    {
        // Arrange
        var fileService = Substitute.For<IReadOnlyFileService>();
        var logger = Substitute.For<ILogger<ExchangeJsonRepository>>();
        var repository = new ExchangeJsonRepository(fileService, logger);

        fileService.ReadAllFromFolder<JsonExchange>().Returns(new List<JsonExchange>());

        // Act
        var result = repository.GetAll();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
    
    [Fact]
    public void GetAll_ShouldReturnEmptyList_WhenFileServiceReturnsNull()
    {
        // Arrange
        var fileService = Substitute.For<IReadOnlyFileService>();
        var logger = Substitute.For<ILogger<ExchangeJsonRepository>>();
        var repository = new ExchangeJsonRepository(fileService, logger);

        fileService.ReadAllFromFolder<JsonExchange>().Returns((List<JsonExchange>?)null);

        // Act
        var result = repository.GetAll();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}