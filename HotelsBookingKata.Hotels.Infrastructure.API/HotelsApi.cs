namespace HotelsBookingKata.Hotels.Infrastructure.API;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

internal record HotelDto(string Id, string Name);
public static class HotelsApi
{
    public static void UseHotelsApi(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapGet("/hotels", () => Results.Ok());
        endpointRouteBuilder.MapGet("/hotels/{id}", );
        endpointRouteBuilder.MapPost("/hotels", (HotelDto hotel) => Results.Ok(hotel));
    }
}