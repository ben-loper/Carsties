using AuctionService.Data;
using AuctionService.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuctionsController(AuctionDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Auction>>> Get()
    {
        return await context.Auctions
            .OrderBy(auction => auction.Item.Make)
            .ThenBy(auction => auction.Item.Model)
            .ToListAsync();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Auction?>> Get(string id)
    {
        var auction = await context.Auctions
            .FirstOrDefaultAsync(auction => auction.Id == id);

        if (auction is null) return StatusCode(StatusCodes.Status404NotFound);

        return auction;
    }
}