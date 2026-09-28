using AuctionService.Data;
using AuctionService.Errors;
using Mapster;
using Microsoft.EntityFrameworkCore;

TypeAdapterConfig.GlobalSettings.Scan(typeof(Program).Assembly);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AuctionDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// Initialize the database because doing this each time would be rough
try
{
    DbInitializer.InitDb(app);
}
catch (Exception e)
{
    Console.WriteLine($"Error initializing the database: {e.Message}");
}

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.MapControllers();

app.Run();