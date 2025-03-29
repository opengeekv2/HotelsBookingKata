using HotelsBookingKata.Hotels.Infrastructure.API;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseHotelsApi();

await app.RunAsync();

public partial class Program;
