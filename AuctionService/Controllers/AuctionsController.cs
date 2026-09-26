using AuctionService.Data;
using AuctionService.Dto;
using AuctionService.Entities;
using AuctionService.RequestHelpers;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuctionsController(AuctionDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<AuctionDto>>> GetAuctions()
    {
        return await context.Auctions
            .OrderBy(auction => auction.Item.Make)
            .ThenBy(auction => auction.Item.Model)
            .ProjectToType<AuctionDto>()
            .ToListAsync();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<AuctionDto>> GetAuctionById(string id)
    {
        var auction = await context.Auctions
            .ProjectToType<AuctionDto>()
            .FirstOrDefaultAsync(auction => auction.Id == id);

        if (auction is null) return StatusCode(StatusCodes.Status404NotFound);

        return auction.Adapt<AuctionDto>();
    }

    [HttpPost]
    public async Task<ActionResult<AuctionDto>> CreateAuction(CreateAuctionRequestDto dto)
    {
        var auction = dto.Adapt<Auction>();
        auction.Seller = "Seller"; //TODO: Seller needs to be populated when authentication is added 
        context.Auctions.Add(auction);

        await context.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetAuctions), new { id = auction.Id }, auction.Adapt<AuctionDto>());
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAuction([FromBody] UpdateAuctionRequestDto dto, string id)
    {
        var auction = await context.Auctions
            .Include(auction => auction.Item)
            .FirstOrDefaultAsync(auction => auction.Id == id);
        
        if (auction is null) return StatusCode(StatusCodes.Status404NotFound);
        
        if (dto.AllValuesNull() || dto.AllValuesMatch(auction)) return StatusCode(StatusCodes.Status204NoContent);
        
        AuctionMap.MapUpdateValues(auction, dto);
        
        await context.SaveChangesAsync();

        return StatusCode(StatusCodes.Status204NoContent);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAuction(string id)
    {
        var auction = await context.Auctions
            .Include(auction => auction.Item)
            .FirstOrDefaultAsync(auction => auction.Id == id);
        
        if (auction is null) return StatusCode(StatusCodes.Status404NotFound);

        if (auction.Seller != "Seller") return StatusCode(StatusCodes.Status401Unauthorized); // TODO: Needs to validate user when authorization is added

        context.Auctions.Remove(auction);
        await context.SaveChangesAsync();
        
        return StatusCode(StatusCodes.Status204NoContent);
    }
}