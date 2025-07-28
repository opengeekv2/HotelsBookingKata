using HotelsBookingKata.Hotel.Domain;
using HotelsBookingKata.Hotels.Infrastructure.API;
using HotelsBookingKata.Hotels.Infrastructure.DB;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.AddDb();
builder.Services.AddScoped<HotelService>();
var app = builder.Build();

app.UseHotelsApi();

await app.RunAsync();

public partial class Program;
