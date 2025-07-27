namespace HotelsBookingKata.Hotels.Infrastructure.API;

using HotelsBookingKata.Hotel.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

internal record HotelDto(string Id, string Name);
public static class HotelsApi
{
    public static void UseHotelsApi(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapGet("/hotels", () => Results.Ok());
        endpointRouteBuilder.MapGet("/hotels/{id}", (string id) => Results.Ok(new HotelDto(id, "a hotel name")));
        endpointRouteBuilder.MapPost("/hotels", (string id, HotelDto hotelDto, HotelService hotelService) => {
            hotelService.AddHotel(id, hotelDto.Name);
            var newHotelDto = hotelService.GetHotel(id);
            return Results.Ok(newHotelDto);
        });
    }
}