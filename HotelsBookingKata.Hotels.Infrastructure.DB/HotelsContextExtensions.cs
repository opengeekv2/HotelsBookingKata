using System.Data.Common;
using HotelsBookingKata.Hotel.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HotelsBookingKata.Hotels.Infrastructure.DB;

public static class HotelsContextExtensions
{
    public static void AddDb(this WebApplicationBuilder builder)
    {
        builder.AddNpgsqlDbContext<HotelsContext>(connectionName: "hotelsdb");
        builder.Services.AddScoped<IHotelRepository, HotelRepository>();
    }
}
