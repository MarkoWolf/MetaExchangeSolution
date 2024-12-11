using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using MetaExchange.Application.Models;
using MetaExchange.WebApi.Contracts.Requests;

namespace MetaExchange.WebApi.Tests;

public class OrderBookControllerTests(MetaExchangeWebApiFactory factory) : IClassFixture<MetaExchangeWebApiFactory>
{
    private readonly HttpClient _httpClient = factory.CreateClient();
    
    [Fact]
    public async Task GetBuyExecutionPlans_ShouldReturnOk_WithValidRequest()
    {
        // Arrange
        var request = new ExecutionPlanRequest { Amount = 10 };

        // Act
        var response = await _httpClient.GetAsync($"/api/OrderBook/executionplans/buy?Amount={request.Amount}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<List<OrderExecutionPlan>>();
        result.Should().NotBeNull();
        result.Should().HaveCountGreaterThan(0);
    }

    [Fact]
    public async Task GetBuyExecutionPlans_ShouldReturnBadRequest_WithInvalidRequest()
    {
        // Arrange
        var request = new ExecutionPlanRequest { Amount = 0 }; // Invalid amount

        // Act
        var response = await _httpClient.GetAsync($"/api/OrderBook/executionplans/buy?Amount={request.Amount}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetSellExecutionPlans_ShouldReturnOk_WithValidRequest()
    {
        // Arrange
        var request = new ExecutionPlanRequest { Amount = 15 };

        // Act
        var response = await _httpClient.GetAsync($"/api/OrderBook/executionplans/sell?Amount={request.Amount}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<List<OrderExecutionPlan>>();
        result.Should().NotBeNull();
        result.Should().HaveCountGreaterThan(0);
    }

    [Fact]
    public async Task GetSellExecutionPlans_ShouldReturnBadRequest_WithInvalidRequest()
    {
        // Arrange
        var request = new ExecutionPlanRequest { Amount = -5 }; // Invalid amount

        // Act
        var response = await _httpClient.GetAsync($"/api/OrderBook/executionplans/sell?Amount={request.Amount}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}