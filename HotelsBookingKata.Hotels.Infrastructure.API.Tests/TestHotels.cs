using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;

namespace HotelsBookingKata.Hotels.Infrastructure.API.Tests;

public class TestHotels(WebApplicationFactory<Program> factory)
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

    [Fact]
    public async Task TestPostHotel()
    {
        // Arrange
        var client = factory.CreateClient();

        // Act
        var postResponse = await client.PostAsync("/hotels", new StringContent(
            JsonSerializer.Serialize(new
            {
                id = "hotel-1",
                name = "Hotel 1"
            }), System.Text.Encoding.UTF8, "application/json"));

        var getResponse = await client.GetAsync("/hotels/hotel-1");
        // Assert
        postResponse.EnsureSuccessStatusCode(); // Status Code 200-299
        var postHotelDto = await postResponse.Content.ReadFromJsonAsync<HotelDto>();
        postHotelDto.ShouldNotBeNull();
        postHotelDto.Id.ShouldBe("hotel-1");
        postHotelDto.Name.ShouldBe("Hotel 1");

        getResponse.EnsureSuccessStatusCode(); // Status Code 200-299
        var getHotelDto = await getResponse.Content.ReadFromJsonAsync<HotelDto>();
        getHotelDto.ShouldNotBeNull();
        getHotelDto.Id.ShouldBe("hotel-1");
        getHotelDto.Name.ShouldBe("Hotel 1");
    }
}
