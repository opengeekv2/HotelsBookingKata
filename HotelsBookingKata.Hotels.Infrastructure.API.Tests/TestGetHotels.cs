using Microsoft.AspNetCore.Mvc.Testing;

namespace HotelsBookingKata.Hotels.Infrastructure.API.Tests;

public class ApiTests(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task TestGetHotels()
    {
        // Arrange
        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync("/hotels");

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
    }
}
