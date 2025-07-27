using HotelsBookingKata.Hotel.Domain;
using HotelsBookingKata.Hotels.Infrastructure.API;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.Services.AddSingleton<HotelService>();
var app = builder.Build();

app.UseHotelsApi();

await app.RunAsync();

public partial class Program;
