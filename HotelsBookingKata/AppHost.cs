var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres");
var kafka = builder.AddKafka("kafka").WithKafkaUI();

var hotelsDb = postgres.AddDatabase("hotels-db");
builder
    .AddProject<Projects.HotelsBookingKata_Hotels_Infrastructure_Boot>("hotels")
    .WithReference(hotelsDb)
    .WaitFor(hotelsDb)
    .WithReference(kafka)
    .WaitFor(kafka);

var companiesDb = postgres.AddDatabase("companies-db");
builder
    .AddProject<Projects.HotelsBookingKata_Company_Infrastructure_Boot>("companies")
    .WithReference(companiesDb)
    .WaitFor(companiesDb)
    .WithReference(kafka)
    .WaitFor(kafka);

var bookingsDb = postgres.AddDatabase("bookings-db");
builder
    .AddProject<Projects.HotelsBookingKata_Book_Infrastructure_Boot>("books")
    .WithReference(bookingsDb)
    .WaitFor(bookingsDb)
    .WithReference(kafka)
    .WaitFor(kafka);

await builder.Build().RunAsync();
