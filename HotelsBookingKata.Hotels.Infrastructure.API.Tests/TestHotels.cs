using System.ComponentModel;
using System.Data.Common;
using System.Net.Http.Json;
using System.Text.Json;
using HotelsBookingKata.Hotels.Infrastructure.DB;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Shouldly;
using Testcontainers.PostgreSql;

namespace HotelsBookingKata.Hotels.Infrastructure.API.Tests;

public class TestHotels
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public TestHotels(WebApplicationFactory<Program> factory)
    {
        var postgreSqlContainer = new PostgreSqlBuilder().Build();
        postgreSqlContainer.StartAsync().GetAwaiter().GetResult();
        factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.AddSingleton<DbConnection>(sp =>
                {
                    var connectionString = postgreSqlContainer.GetConnectionString();
                    var dbConnection = new NpgsqlConnection(connectionString);
                    dbConnection.Open();
                    return dbConnection;
                });
                services.AddDbContext<HotelsContext>((container, options) =>
                {
                    options.UseNpgsql(container.GetRequiredService<DbConnection>());
                });
            });
        });
        _factory = factory;
    }

    [Fact]
    public async Task TestGetHotels()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/hotels");

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
    }

    [Fact]
    public async Task TestPostHotel()
    {
        // Arrange
        var client = _factory.CreateClient();

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
