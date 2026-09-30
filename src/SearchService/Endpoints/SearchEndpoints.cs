using Meilisearch;
using SearchService.Models;

namespace SearchService.Endpoints;

public static class SearchEndpoints
{
    public static async Task<IResult> GetSearchResults(
        MeilisearchClient client, 
        string? searchTerm,
        int pageNumber = 1,
        int pageSize = 10
        )
    {
        var query = new SearchQuery()
        {
            Page = pageNumber < 1 ? 1 : pageNumber,
            HitsPerPage = pageSize > 50 ? 50 : pageSize
        };
        
        var results = (PaginatedSearchResult<Item>) await client.Index("items").SearchAsync<Item>(searchTerm ?? string.Empty, query);
        return Results.Ok(new
        {
            results = results.Hits,
            pageCount = results.TotalPages,
            totalCount = results.TotalHits
        });
    }
}