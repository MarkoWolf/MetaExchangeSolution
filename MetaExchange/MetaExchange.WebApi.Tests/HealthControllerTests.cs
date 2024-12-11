using System.Net;
using FluentAssertions;

namespace MetaExchange.WebApi.Tests;

public class HealthControllerTests(MetaExchangeWebApiFactory factory) : IClassFixture<MetaExchangeWebApiFactory>
{
    private readonly HttpClient _httpClient = factory.CreateClient();

    [Fact]
    public async Task GetHealth_Should_return_200_OK()
    {
        // Arrange 
        
        string healthEndpointUrl = "/api/health";
        HttpStatusCode expecteStatusCode = HttpStatusCode.OK;
        
        // Act 
        var response = await _httpClient.GetAsync(healthEndpointUrl);
        
        // Assert 
        response.StatusCode.Should().Be(expecteStatusCode);
    }
 

}