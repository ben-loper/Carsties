using System.Text.Json;
using Meilisearch;
using SearchService.Models;

namespace SearchService.Data;

public static class DbInitializer
{
    private const string IndexUid = "items";

    public static async Task InitDb(WebApplication app)
    {
        var client = app.Services.GetRequiredService<MeilisearchClient>();
        
        if (await IndexHasDocuments(client))
        {
            Console.WriteLine("Search index already has documents. Seeding skipped");
            return;
        }

        var items = await GetSeededItems();
        
        if (items == null || items.Count == 0)
        {
            Console.WriteLine("No items found. Seeding skipped");
            return;
        }

        await SeedIndex(client, items);
        
        Console.WriteLine($"Search index populated with {items.Count} items");
    }

    private static async Task<bool> IndexHasDocuments(MeilisearchClient client)
    {
        var indexes = await client.GetAllIndexesAsync();
        
        var itemIndex = indexes.Results.FirstOrDefault(index => index.Uid == IndexUid);
        if (itemIndex == null) return false; // No index exists with the Uid items
        
        var stats = await itemIndex.GetStatsAsync();
        return stats.NumberOfDocuments > 0;
    }
    
    private static async Task<List<Item>?> GetSeededItems()
    {
        var options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };
        
        var path = Path.Combine(AppContext.BaseDirectory, "Data", "auctions.json");

        await using var stream = File.OpenRead(path);
        return await JsonSerializer.DeserializeAsync <List<Item>>(stream, options);
    }

    private static async Task SeedIndex(MeilisearchClient client, List<Item> items)
    {
        var index = client.Index(IndexUid);
        var addTask = await index.AddDocumentsAsync(items, primaryKey: "id");
        await client.WaitForTaskAsync(addTask.TaskUid);

        var settingTask = await index.UpdateSettingsAsync(new Settings()
        {
            SearchableAttributes = ["make", "model", "description"],
            FilterableAttributes = ["seller", "winner", "status", "auctionEnd"],
            SortableAttributes = ["auctionEnd", "currentHighBid", "createdAt", "updatedAt", "make", "model"]
        });

        await client.WaitForTaskAsync(settingTask.TaskUid);
    }
}