namespace HotelsBookingKata.Hotels.Infrastructure.API;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

public static class HotelsApi
{
    public static void UseHotelsApi(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapGet("/hotels", () => Results.Ok());
    }
}
