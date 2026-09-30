using Meilisearch;
using SearchService.Data;
using SearchService.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    return new MeilisearchClient(
        config["Meilisearch:Url"],
        config["Meilisearch:ApiKey"]
    );
});

var app = builder.Build();
await DbInitializer.InitDb(app);

// Configure the HTTP request pipeline.
app.MapGet("/api/search", SearchEndpoints.GetSearchResults);

app.Run();
